using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
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
        private IList<ContentTypeSort> _allowedChildNodeTypes;
        private IList<PropertyType> _propertyTypes;

        public DocumentTypeTestBuilder(string alias)
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            ContentTypeService = new Mock<IContentTypeService>();
            _dataTypeService = new Mock<IDataTypeService>();
            _allowedChildNodeTypes = new List<ContentTypeSort>();
            _propertyTypes = new List<PropertyType>();

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

        public DocumentTypeTestBuilder SetupExistingDocumentType(string alias, int id, int parentId)
        {
            ContentType = CreateNewMockContentType(alias, id, parentId);
            ContentTypeService.Setup(x => x.GetContentType(alias)).Returns(ContentType.Object);

            return this;
        }

        public DocumentTypeTestBuilder SetupNewDocumentType(string alias, int id, int parentId)
        {
            ContentType = CreateNewMockContentType(alias, id, parentId);
            UmbracoFactory.Setup(x => x.NewContentType(parentId)).Returns(ContentType.Object);

            return this;
        }

        private Mock<IContentType> CreateNewMockContentType(string alias, int id, int parentId)
        {
            var contentType = new Mock<IContentType>();
            contentType.SetupAllProperties();
            contentType.SetupGet(x => x.Alias).Returns(alias);
            contentType.SetupGet(x => x.Id).Returns(id);
            contentType.SetupGet(x => x.ParentId).Returns(parentId);

            
            var propertyGroups = new PropertyGroupCollection(new List<PropertyGroup>());
            contentType.SetupGet(x => x.PropertyGroups).Returns(propertyGroups);

            return contentType;
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

        public DocumentTypeTestBuilder HasAllowedContentType(string alias, int id)
        {
            ReturnsExistingContentType(alias, id);
            var contentType = new Mock<IContentType>();
            contentType.SetupGet(x => x.Id).Returns(id);
            contentType.SetupGet(x => x.Alias).Returns(alias);

            ContentTypeService.Setup(x => x.GetContentType(alias)).Returns(contentType.Object);
            _allowedChildNodeTypes.Add(new ContentTypeSort(id, _allowedChildNodeTypes.Count + 1));

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
            ContentType.Object.AllowedContentTypes = _allowedChildNodeTypes;
            return _documentTypeBuilder;
        }

        public DocumentTypeTestBuilder AddExistingTab(string name, int sortOrder)
        {
            var propertyGroup = new PropertyGroup
            {
                Name = name,
                SortOrder = sortOrder
            };

            ContentType.Object.PropertyGroups.Add(propertyGroup);

            return this;
        }

        public DocumentTypeTestBuilder AllowAtRoot()
        {
            ContentType.Object.AllowedAsRoot = true;
            return this;
        }

        public DocumentTypeTestBuilder AddField(string alias)
        {
            ContentType.Setup(x => x.PropertyTypeExists(alias)).Returns(true);

            var dataDefinition = new Mock<IDataTypeDefinition>();
            var propertyType = new PropertyType(dataDefinition.Object);

            UmbracoFactory.Setup(x => x.GetPropertyType(It.IsAny<IContentType>(), alias)).Returns(propertyType);

            return this;
        }
    }
}
