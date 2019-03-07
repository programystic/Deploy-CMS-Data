using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using System;
using Umbraco.Core.Models.Entities;
using Umbraco.Core.Services;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.UmbracoCms.UnitTest.Builders
{
    internal class DocumentTypeFolderTestBuilder
    {
        private readonly DocumentTypeFolderBuilder _documentTypeFolderBuilder;
        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeFolderTestBuilder(string name)
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            Mock<IContentTypeService> contentTypeService = new Mock<IContentTypeService>();
            _documentTypeFolderBuilder = new DocumentTypeFolderBuilder(contentTypeService.Object, UmbracoFactory.Object, name);

        }

        public DocumentTypeFolderTestBuilder ReturnsExistingFolder(string folderName, int level)
        {
            Mock<IUmbracoEntity> entity = new Mock<IUmbracoEntity>();
            UmbracoFactory.Setup(x => x.GetContainer(folderName, level))
                .Returns(entity.Object);

            return this;
        }

        public DocumentTypeFolderTestBuilder ReturnsNewFolder(string folderName)
        {
            Mock<IUmbracoEntity> entity = new Mock<IUmbracoEntity>();
            entity.SetupProperty(x => x.Name, folderName);

            UmbracoFactory.Setup(x => x.NewContainer(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(entity.Object);

            return this;
        }

        public DocumentTypeFolderBuilder Build()
        {
            return _documentTypeFolderBuilder;
        }

    }
}
