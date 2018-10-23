using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Builders
{
    internal class DocumentTypeFolderTestBuilder
    {
        private readonly DocumentTypeFolderBuilder _documentTypeFolderBuilder;
        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeFolderTestBuilder()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            var contentTypeService = new Mock<IContentTypeService>();
            _documentTypeFolderBuilder = new DocumentTypeFolderBuilder(contentTypeService.Object, UmbracoFactory.Object);

        }

        public DocumentTypeFolderTestBuilder ReturnsExistingFolder(string folderName, int level)
        {
            var entity = new Mock<IUmbracoEntity>();
            UmbracoFactory.Setup(x => x.GetContainer(folderName, level))
                .Returns(entity.Object);

            return this;
        }

        public DocumentTypeFolderTestBuilder ReturnsNewFolder(string folderName)
        {
            var entity = new Mock<IUmbracoEntity>();
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
