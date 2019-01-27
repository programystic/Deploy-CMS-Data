﻿using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using System;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
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

            Assert.Throws<ArgumentException>(
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
                .ReturnsNewContentType(ParentId)
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
                .ReturnsNewContentType(Constants.Umbraco.RootFolder)
                .Build();

            Umbraco.Core.Models.IContentType documentType = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .BuildAtRoot();

            Assert.AreEqual(Alias, documentType.Alias);
            Assert.AreEqual(Name, documentType.Name);
            Assert.AreEqual(Description, documentType.Description);
            Assert.AreEqual(Icon, documentType.Icon);
            Assert.AreEqual(UmbracoCms.Constants.Umbraco.RootFolder, documentType.ParentId);
            Assert.IsTrue(documentType.AllowedAsRoot);
            Assert.IsFalse(documentType.IsContainer);
        }

        [Test]
        public static void CreateInFolderWithLevel()
        {
            var builder = new DocumentTypeTestBuilder(Alias)
                .ReturnsNewContentType(ParentFolderId)
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
                .ReturnsNewContentType(ParentFolderId)
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
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .ReturnsNewContentType(ParentFolderId)
                .ReturnsFolder(ParentFolderName, ParentFolderLevel, ParentFolderId)
                .Build();

            var folder = builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .AddAllowedChildNodeType("homePage")
                .AddAllowedChildNodeType("abourPage")
                .BuildInFolder(ParentFolderName);

            Assert.AreEqual(Name, folder.Name);
        }
    }
}