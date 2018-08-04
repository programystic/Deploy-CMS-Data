using System;
using DeployCmsData.Constants;
using DeployCmsData.Test.Services;
using NUnit.Framework;

namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public class DocumentTypeTests
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";
        private const int ParentId = 101;
        private const string ParentFolderName = "parentFolderName";
        private const int ParentFolderLevel = 25;
        private const int ParentFolderId = 78;

        [Test]
        public void DocumentTypeCreateWithInvalidParent()
        {
            var builder = new DocumentTypeBuilderBuilder().Build();

            Assert.Throws<ArgumentException>(
                () => builder
                    .Alias(Alias)
                    .Icon(Icon)
                    .Name(Name)
                    .Description(Description)
                    .Build());
        }

        [Test]
        public void DocumentTypeCreateWithParent()
        {
            var builder = new DocumentTypeBuilderBuilder()
                .ReturnsNewContentType(ParentId, ParentAlias)
                .Build();

            var documentType = builder
                .Alias(Alias)
                .ParentAlias(ParentAlias)
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .Build();

            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.AreEqual(ParentId, documentType.ParentId);
            Assert.IsFalse(documentType.AllowedAsRoot);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public void DocumentTypeCreateAtRoot()
        {
            var builder = new DocumentTypeBuilderBuilder()
                .ReturnsNewContentType(FolderConstants.RootFolder, ParentAlias)
                .Build();

            var documentType = builder
                .Alias(Alias)
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildAtRoot();

            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.AreEqual(FolderConstants.RootFolder, documentType.ParentId);
            Assert.IsTrue(documentType.AllowedAsRoot);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public void DocumentTypeCreateInFolder()
        {
            var builder = new DocumentTypeBuilderBuilder()
                .ReturnsNewContentType(ParentFolderId, ParentAlias)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var documentType = builder
                .Alias(Alias)
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
    }
}