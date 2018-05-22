using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Services
{
    internal class DocumentTypeTestBuilder
    {
        private readonly CreateDocumentTypeBuilder _documentTypeBuilder;
        private readonly Mock<IContentTypeService> _contentTypeService;      

        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeTestBuilder()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            _contentTypeService = new Mock<IContentTypeService>();
            _documentTypeBuilder = new CreateDocumentTypeBuilder(_contentTypeService.Object, UmbracoFactory.Object);
        }

        public DocumentTypeTestBuilder ReturnsNewContentType(int parentId, string parentAlias)
        {
            var contentType = new Mock<IContentType>();
            contentType.SetupAllProperties();
            contentType.Setup(x => x.ParentId).Returns(parentId);

            contentType.Setup(x => x.ParentId).Returns(parentId);
            UmbracoFactory.Setup(x => x.NewContentType(parentId)).Returns(contentType.Object);

            var parentContentType = new Mock<IContentType>();
            parentContentType.SetupGet(x => x.Alias).Returns(parentAlias);
            parentContentType.SetupGet(x => x.Id).Returns(parentId);

            _contentTypeService.Setup(x => x.GetContentType(parentAlias)).Returns(parentContentType.Object);

            return this;
        }       

        public DocumentTypeTestBuilder CreateDocumentTypeFolderAtRoot(int id, string name)
        {
            var newEntity = new Mock<IUmbracoEntity>();
            newEntity.SetupAllProperties();
            newEntity.Setup(x => x.Id).Returns(id);

            UmbracoFactory.Setup(x => x.NewContainer(CmsContentValues.RootFolder, name, 1)).Returns(newEntity.Object);

            var parentContentType = new Mock<IContentType>();
            parentContentType.Setup(x => x.Id).Returns(CmsContentValues.RootFolder);

            return this;
        }

        public CreateDocumentTypeBuilder Build()
        {
            return _documentTypeBuilder;
        }
    }
}
