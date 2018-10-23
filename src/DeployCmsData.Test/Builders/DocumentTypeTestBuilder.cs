using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Builders
{
    internal class DocumentTypeTestBuilder
    {
        private readonly DocumentTypeBuilder _documentTypeBuilder;
        private readonly Mock<IContentTypeService> _contentTypeService;
        private readonly Mock<IDataTypeService> _dataTypeService;
        public Mock<IUmbracoFactory> UmbracoFactory { get; }
        public Mock<IContentType> ContentType = new Mock<IContentType>();

        public DocumentTypeTestBuilder()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            _contentTypeService = new Mock<IContentTypeService>();   
            _dataTypeService = new Mock<IDataTypeService>();

            _documentTypeBuilder = new DocumentTypeBuilder(
                _contentTypeService.Object, 
                UmbracoFactory.Object,
                _dataTypeService.Object);

            var dataTypeDefinition = new Mock<IDataTypeDefinition>();
            var propertyType = new Mock<PropertyType>(dataTypeDefinition.Object);
            UmbracoFactory.Setup(x => x.NewPropertyType(It.IsAny<IDataTypeDefinition>(), It.IsAny<string>()))
                .Returns(propertyType.Object);            
        }

        public DocumentTypeTestBuilder ReturnsNewContentType(int parentId)
        {
            ContentType = new Mock<IContentType>();
            ContentType.SetupAllProperties();
            ContentType.Setup(x => x.ParentId).Returns(parentId);

            ContentType.Setup(x => x.ParentId).Returns(parentId);
            UmbracoFactory.Setup(x => x.NewContentType(parentId)).Returns(ContentType.Object);            

            return this;
        }

        public DocumentTypeTestBuilder ReturnsExistingContentType(string alias, int id = 0)
        {
            var contentType = new Mock<IContentType>();
            contentType.SetupGet(x => x.Alias).Returns(alias);
            contentType.SetupGet(x => x.Id).Returns(id);

            _contentTypeService.Setup(x => x.GetContentType(alias)).Returns(contentType.Object);

            return this;
        }

        public DocumentTypeTestBuilder ReturnsFolder(string folderName, int folderLevel, int folderId)
        {
            var folder = new Mock<IUmbracoEntity>();
            folder.SetupAllProperties();
            folder.Setup(x => x.Name).Returns(folderName);
            folder.Setup(x => x.Level).Returns(folderLevel);
            folder.Setup(x => x.Id).Returns(folderId);

            UmbracoFactory.Setup(x => x.GetContainer(folderName, folderLevel)).Returns(folder.Object);

            return this;
        }

        public DocumentTypeTestBuilder ReturnsDataType(CmsDataType dataType)
        {
            var dataTypeDefinition = new Mock<IDataTypeDefinition>();

            _dataTypeService.Setup(x => x.GetDataTypeDefinitionByName(dataType.ToString()))
                .Returns(dataTypeDefinition.Object);

            return this;
        }

        public DocumentTypeBuilder Build()
        {
            return _documentTypeBuilder;
        }
    }
}
