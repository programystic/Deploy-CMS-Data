using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Moq;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Services
{
    internal class DocumentTypeFolderBuilderSetup
    {
        private readonly DocumentTypeFolderBuilder _documentTypeFolderBuilder;
        private readonly Mock<IContentTypeService> _contentTypeService;
        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeFolderBuilderSetup()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            _contentTypeService = new Mock<IContentTypeService>();
            _documentTypeFolderBuilder = new DocumentTypeFolderBuilder(_contentTypeService.Object, UmbracoFactory.Object);

        }

        // var parent = _factory.GetContainer(parentFolderName, parentFolderLevel);
        public DocumentTypeFolderBuilderSetup ReturnsExistingFolder(string folderName, int level)
        {
            var entity = new Mock<IUmbracoEntity>();
            UmbracoFactory.Setup(x => x.GetContainer(folderName, level))
                .Returns(entity.Object);

            return this;
        }

        public DocumentTypeFolderBuilderSetup ReturnsNewFolder(string folderName)
        {
            var entity = new Mock<IUmbracoEntity>();
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
