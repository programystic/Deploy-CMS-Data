using Castle.DynamicProxy.Contributors;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Services
{
    internal class DocumentTypeBuilderBuilder
    {
        private readonly DocumentTypeBuilder _documentTypeBuilder;
        private readonly Mock<IContentTypeService> _contentTypeService;      
        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeBuilderBuilder()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            _contentTypeService = new Mock<IContentTypeService>();
            _documentTypeBuilder = new DocumentTypeBuilder(_contentTypeService.Object, UmbracoFactory.Object);
        }

        public DocumentTypeBuilderBuilder ReturnsNewContentType(int parentId, string parentAlias)
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

        public DocumentTypeBuilderBuilder ReturnsFolder(string folderName, int folderLevel, int folderId)
        {
            var folder = new Mock<IUmbracoEntity>();
            folder.SetupAllProperties();
            folder.Setup(x => x.Name).Returns(folderName);
            folder.Setup(x => x.Level).Returns(folderLevel);
            folder.Setup(x => x.Id).Returns(folderId);

            UmbracoFactory.Setup(x => x.GetContainer(folderName, folderLevel)).Returns(folder.Object);

            return this;
        }

        //public DocumentTypeBuilderBuilder CreateDocumentTypeFolderAtRoot(int id, string name)
        //{
        //    var newEntity = new Mock<IUmbracoEntity>();
        //    newEntity.SetupAllProperties();
        //    newEntity.Setup(x => x.Id).Returns(id);

        //    UmbracoFactory.Setup(x => x.NewContainer(FolderConstants.RootFolder, name, 1)).Returns(newEntity.Object);

        //    var parentContentType = new Mock<IContentType>();
        //    parentContentType.Setup(x => x.Id).Returns(FolderConstants.RootFolder);

        //    return this;
        //}

        public DocumentTypeBuilder Build()
        {
            return _documentTypeBuilder;
        }
    }
}
