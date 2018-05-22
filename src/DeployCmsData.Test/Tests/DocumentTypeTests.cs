using System;
using DeployCmsData.Constants;
using DeployCmsData.Test.Services;
using NUnit.Framework;

namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public static class DocumentTypeTests
    {
        [Test]
        public static void DocumentTypeCreateWithInvalidParent()
        {
            const string parentAlias = "myParentAlias";
            const string alias = "myAlias";
            const string name = "myName";
            const string description = "myDescription";
            const string icon = "myIcon";
            const bool isAllowedAtRoot = true;

            var builder = new DocumentTypeTestBuilder().Build();

            Assert.Throws<ArgumentException>(
                () => builder.CreateDocumentType(parentAlias, alias, icon, name, description, isAllowedAtRoot));
        }

        [Test]
        public static void DocumentTypeCreate()
        {
            const string parentAlias = "myParentAlias";
            const int parentId = 101;
            const string alias = "myAlias";
            const string name = "myName";
            const string description = "myDescription";
            const string icon = "myIcon";
            const bool isAllowedAtRoot = true;

            var builder = new DocumentTypeTestBuilder()
                .ReturnsNewContentType(parentId, parentAlias)
                .Build();            

            var documentType = builder.CreateDocumentType(parentAlias, alias, icon, name, description, isAllowedAtRoot);

            Assert.AreEqual(alias, documentType.Alias);
            Assert.AreEqual(name, documentType.Name);
            Assert.AreEqual(description, documentType.Description);
            Assert.AreEqual(icon, documentType.Icon);
            Assert.AreEqual(parentId, documentType.ParentId);
            Assert.AreEqual(isAllowedAtRoot, documentType.AllowedAsRoot);
            Assert.AreEqual(false, documentType.IsContainer);
        }

        [Test]
        public static void DocumentTypeCreateAtRoot()
        {
            const string alias = "myAlias";
            const string parentAlias = "myParentAlias";
            const string name = "myName";
            const string description = "myDescription";
            const string icon = "myIcon";
            const bool isAllowedAtRoot = true;

            var builder = new DocumentTypeTestBuilder()
                .ReturnsNewContentType(CmsContentValues.RootFolder, parentAlias)
                .Build();

            var documentType = builder.CreateDocumentTypeAtRoot(alias, icon, name, description, isAllowedAtRoot);

            Assert.AreEqual(alias, documentType.Alias);
            Assert.AreEqual(name, documentType.Name);
            Assert.AreEqual(description, documentType.Description);
            Assert.AreEqual(icon, documentType.Icon);
            Assert.AreEqual(CmsContentValues.RootFolder, documentType.ParentId);
            Assert.AreEqual(isAllowedAtRoot, documentType.AllowedAsRoot);
            Assert.AreEqual(false, documentType.IsContainer);
        }

        [Test]
        public static void DocumentTypeCreateInFolder()
        {
            const int folderId = 101;
            const string alias = "myAlias";
            const string parentAlias = "myParentAlias";
            const string name = "myName";
            const string description = "myDescription";
            const string icon = "myIcon";
            const bool isAllowedAtRoot = true;

            var builder = new DocumentTypeTestBuilder()
                .ReturnsNewContentType(folderId, parentAlias)              
                .Build();

            var documentType = builder.CreateDocumentType(folderId, alias, icon, name, description, isAllowedAtRoot);

            Assert.AreEqual(alias, documentType.Alias);
            Assert.AreEqual(name, documentType.Name);
            Assert.AreEqual(description, documentType.Description);
            Assert.AreEqual(icon, documentType.Icon);
            Assert.AreEqual(isAllowedAtRoot, documentType.AllowedAsRoot);
            Assert.AreEqual(false, documentType.IsContainer);
        }
    }
}
