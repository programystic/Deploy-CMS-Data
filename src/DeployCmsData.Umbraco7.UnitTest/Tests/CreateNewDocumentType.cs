using DeployCmsData.Umbraco7.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Umbraco.Core.Models;

namespace DeployCmsData.Umbraco7.UnitTest.Tests
{
    public static class CreateNewDocumentType
    {
        private const int Id = 999;
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";
        private const int ParentId = 101;
        private const string ParentFolderName = "parentFolderName";
        private const int ParentFolderLevel = 7;
        private const int ParentFolderId = 78;

        [Test]
        public static void CreateWithInvalidParent()
        {
            var builder = new DocumentTypeTestBuilder(Alias).Build();

            Assert.Throws<InvalidOperationException>(
                () => builder
                    .Icon(Icon)
                    .Name(Name)
                    .Description(Description)
                    .BuildWithParent(ParentAlias));
        }

        [Test]
        public static void CreateWithParent()
        {
            var builder = new DocumentTypeTestBuilder(Alias)
                .SetupNewDocumentType(Alias, Id, ParentId)
                .ReturnsExistingContentType(ParentAlias, ParentId)
                .Build();

            var documentType = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildWithParent(ParentAlias);

            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.AreEqual(ParentId, documentType.ParentId);
            Assert.IsFalse(documentType.AllowedAsRoot);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public static void CreateAtRoot()
        {
            var builder = new DocumentTypeTestBuilder(Alias)
                .SetupNewDocumentType(Alias,Id, Constants.Umbraco.RootFolder)
                .ReturnsExistingContentType("RootFolder", Constants.Umbraco.RootFolder)
                .Build();

            var documentType = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildAtRoot();

            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.AreEqual(Constants.Umbraco.RootFolder, documentType.ParentId);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public static void CreateInFolderWithLevel()
        {
            var builder = new DocumentTypeTestBuilder(Alias)
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var documentType = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildInFolder(ParentFolderName, ParentFolderLevel);

            Assert.AreEqual(ParentFolderId, documentType.ParentId);
            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.IsFalse(documentType.AllowedAsRoot);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public static void CreateInFolderWithNoLevel()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var folder = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildInFolder(ParentFolderName);

            Assert.AreEqual(ParentFolderId, folder.ParentId);
            Assert.AreEqual(Alias, folder.Alias);
            Assert.AreEqual(Name, folder.Name);
            Assert.AreEqual(Description, folder.Description);
            Assert.AreEqual(Icon, folder.Icon);
            Assert.IsFalse(folder.AllowedAsRoot);
            Assert.IsFalse(folder.IsContainer);

            setup.UmbracoFactory.Verify(x =>
                    x.GetContainer(
                        ParentFolderName,
                        It.IsAny<int>()),
                Times.Exactly(ParentFolderLevel));
        }

        [Test]
        public static void SetAllowedChildNodeTypes()
        {
            const string childType1 = "pageType01";
            const string childType2 = "pageType02";
            const string childType3 = "pageType03";
            const int childId1 = 10;
            const int childId2 = 20;
            const int childId3 = 30;

            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .ReturnsExistingContentType(childType1, childId1)
                .ReturnsExistingContentType(childType2, childId2)
                .ReturnsExistingContentType(childType3, childId3)
                .Build();

            var documentType = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .AddAllowedChildNodeType(childType1)
                .AddAllowedChildNodeType(childType2)
                .AddAllowedChildNodeType(childType3)
                .BuildInFolder(ParentFolderName);

            Assert.AreEqual(3, documentType.AllowedContentTypes.Count());
            Assert.AreEqual(childId1, documentType.AllowedContentTypes.First().Id.Value);
            Assert.AreEqual(childId2, documentType.AllowedContentTypes.Skip(1).First().Id.Value);
            Assert.AreEqual(childId3, documentType.AllowedContentTypes.Skip(2).First().Id.Value);
            Assert.IsTrue(builder.AllowedContentTypesAreEqual(documentType.AllowedContentTypes));
        }

        [Test]
        public static void SetDefaultTemplate()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var template = new Mock<ITemplate>();

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .DefaultTemplate(template.Object)
                .BuildInFolder(ParentFolderName);

            setup.ContentType.Verify(x => x.SetDefaultTemplate(template.Object), Times.Once);
        }

        [Test]
        public static void SetIconNoColour()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var docType = builder
                .Icon("icon-wheel")
                .Name(Name)
                .Description(Description)
                .BuildInFolder(ParentFolderName);

            Assert.AreEqual("icon-wheel", docType.Icon);
        }

        [Test]
        public static void SetIconColour()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var docType = builder
                .Icon("icon-wheel", "colour-blue-green")
                .Name(Name)
                .Description(Description)
                .BuildInFolder(ParentFolderName);

            Assert.AreEqual("icon-wheel colour-blue-green", docType.Icon);
        }

        [Test]
        public static void AllowAtRoot()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var docType = builder
                .AllowedAsRoot()
                .BuildInFolder(ParentFolderName);

            Assert.IsTrue(docType.AllowedAsRoot);
        }

        [Test]
        public static void DoNotAllowAtRoot()
        {
            var builder = new DocumentTypeTestBuilder(Alias)
                .SetupNewDocumentType(Alias, Id, ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var docType = builder
                .BuildInFolder(ParentFolderName);

            Assert.IsFalse(docType.AllowedAsRoot);
        }

        [Test]
        public static void AddInheritedCompositions()
        {
            var testBuilder = new DocumentTypeTestBuilder(Alias)
                .SetupNewDocumentType(Alias, Id, 444)
                .ReturnsExistingContentType("contentType1", 111)
                .ReturnsExistingContentType("contentType2", 222, 111)
                .ReturnsExistingContentType("contentType3", 333, 222)
                .ReturnsExistingContentType("contentType4", 444, 333);

            var builder = testBuilder.Build();
            var docType = builder.BuildWithParent("contentType4");

            testBuilder.ContentType.Verify(x => 
                x.AddContentType(It.IsAny<IContentTypeComposition>()), Times.Exactly(4));
        }
    }
}