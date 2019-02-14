using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using System;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Builders
{
    internal class DocumentTypeTestBuilder
    {
        public Mock<IUmbracoFactory> UmbracoFactory { get; }
        public Mock<IContentType> ContentType = new Mock<IContentType>();
        public readonly Mock<IContentTypeService> ContentTypeService;

        private readonly DocumentTypeBuilder _documentTypeBuilder;
        private readonly Mock<IDataTypeService> _dataTypeService;

        public DocumentTypeTestBuilder(string alias)
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            ContentTypeService = new Mock<IContentTypeService>();
            _dataTypeService = new Mock<IDataTypeService>();

            _documentTypeBuilder = new DocumentTypeBuilder(
                ContentTypeService.Object,
                UmbracoFactory.Object,
                _dataTypeService.Object,
                alias);

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

        public DocumentTypeTestBuilder ReturnsDefaultContentType(string alias, int id = 0)
        {
            ContentType = new Mock<IContentType>();
            ContentType.SetupAllProperties();
            ContentType.SetupGet(x => x.Alias).Returns(alias);
            ContentType.SetupGet(x => x.Id).Returns(id);

            ContentTypeService.Setup(x => x.GetContentType(alias)).Returns(ContentType.Object);

            return this;
        }

        public DocumentTypeTestBuilder ReturnsExistingContentType(string alias, int id = 0)
        {
            var contentType = new Mock<IContentType>();
            contentType.SetupAllProperties();
            contentType.SetupGet(x => x.Alias).Returns(alias);
            contentType.SetupGet(x => x.Id).Returns(id);

            ContentTypeService.Setup(x => x.GetContentType(alias)).Returns(contentType.Object);            

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

        public DocumentTypeTestBuilder ReturnsDataType(Guid dataTypeId)
        {
            var dataTypeDefinition = new Mock<IDataTypeDefinition>();
            dataTypeDefinition.Setup(x => x.Key).Returns(dataTypeId);

            _dataTypeService.Setup(x => x.GetDataTypeDefinitionById(dataTypeId))
                .Returns(dataTypeDefinition.Object);

            return this;
        }

        public DocumentTypeBuilder Build()
        {
            return _documentTypeBuilder;
        }
    }
}
