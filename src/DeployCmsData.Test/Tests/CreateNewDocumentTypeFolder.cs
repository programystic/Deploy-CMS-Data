using DeployCmsData.Constants;
using DeployCmsData.Test.Services;
using Moq;
using NUnit.Framework;
using System;

namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public class CreateNewDocumentTypeFolder
    {
        [Test]
        public void CreateFolderAtRoot()
        {
            const string folderName = "My New Folder";

            var setup = new DocumentTypeFolderBuilderSetup();
            var builder = setup
                .ReturnsNewFolder(folderName)
                .Build();

            var folder = builder
                .Name(folderName)
                .BuildAtRoot();

            setup.UmbracoFactory.Verify(x => x.NewContainer(ValueConstants.RootFolder, folderName, 1), Times.Once);
            Assert.IsNotNull(folder);
        }

        [Test]
        public void CreateFolderWithParentFolderAndLevel()
        {
            const string folderName = "My new folder";
            const string parentFolderName = "Parent folder name";
            const int parentFolderLevel = 7;

            var setup = new DocumentTypeFolderBuilderSetup();
            var builder = setup
                .ReturnsNewFolder(folderName)
                .ReturnsExistingFolder(parentFolderName, parentFolderLevel)
                .Build();

            var folder = builder
                .Name(folderName)
                .BuildWithParentFolder(parentFolderName, parentFolderLevel);

            setup.UmbracoFactory.Verify(x => x.NewContainer(It.IsAny<int>(), folderName, parentFolderLevel), Times.Once);
            Assert.IsNotNull(folder);
        }

        [Test]
        public void CreateFolderWithParentFolderAndNoLevel()
        {
            const string folderName = "My new folder";
            const string parentFolderName = "Parent folder name";
            const int parentFolderLevel = 7;

            var setup = new DocumentTypeFolderBuilderSetup();
            var builder = setup
                .ReturnsNewFolder(folderName)
                .ReturnsExistingFolder(parentFolderName, parentFolderLevel)
                .Build();

            var folder = builder
                .Name(folderName)
                .BuildWithParentFolder(parentFolderName);

            setup.UmbracoFactory.Verify(x => x.NewContainer(It.IsAny<int>(), folderName, parentFolderLevel), Times.Once);
            Assert.IsNotNull(folder);
        }

        [Test]
        public void DocumentTypeFolderCreateWithInvalidParentAndLevel()
        {
            const string folderName = "My new folder";
            const string parentFolderName = "Parent folder name";

            var setup = new DocumentTypeFolderBuilderSetup();
            var builder = setup
                .ReturnsNewFolder(folderName)
                .Build();

            Assert.Throws<ArgumentException>(() =>
                builder.
                    Name(folderName).
                    BuildWithParentFolder(parentFolderName, 1));
        }

        [Test]
        public void DocumentTypeFolderCreateWithInvalidParentNoLevel()
        {
            const string folderName = "My new folder";
            const string parentFolderName = "Parent folder name";

            var setup = new DocumentTypeFolderBuilderSetup();
            var builder = setup
                .ReturnsNewFolder(folderName)
                .Build();

            Assert.Throws<ArgumentException>(() =>
                builder.
                    Name(folderName).
                    BuildWithParentFolder(parentFolderName));
           
            setup.UmbracoFactory.Verify(x =>
                    x.GetContainer(
                        parentFolderName,
                        It.IsAny<int>()),
                Times.Exactly(ValueConstants.MaximumFolderLevel));
        }
    }
}