using System.Collections.Generic;
using Castle.DynamicProxy.Contributors;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Services
{
    internal class DocumentTypeBuilderSetup
    {
        private readonly DocumentTypeBuilder _documentTypeBuilder;
        private readonly Mock<IContentTypeService> _contentTypeService;
        private readonly Mock<IDataTypeService> _dataTypeService;
        public Mock<IUmbracoFactory> UmbracoFactory { get; }

        public DocumentTypeBuilderSetup()
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

        public DocumentTypeBuilderSetup ReturnsNewContentType(int parentId, string parentAlias)
        {
            var contentType = new Mock<IContentType>();
            contentType.SetupAllProperties();
            contentType.Setup(x => x.ParentId).Returns(parentId);

            contentType.Setup(x => x.ParentId).Returns(parentId);
            UmbracoFactory.Setup(x => x.NewContentType(parentId)).Returns(contentType.Object);

            var parentContentType = new Mock<IContentType>();
            parentContentType.SetupGet(x => x.Alias).Returns(parentAlias);
            parentContentType.SetupGet(x => x.Id).Returns(parentId);

            var groups = new List<PropertyGroup>();
            parentContentType.Setup(x => x.PropertyGroups).Returns(new PropertyGroupCollection(groups));

            _contentTypeService.Setup(x => x.GetContentType(parentAlias)).Returns(parentContentType.Object);

            return this;
        }

        public DocumentTypeBuilderSetup ReturnsFolder(string folderName, int folderLevel, int folderId)
        {
            var folder = new Mock<IUmbracoEntity>();
            folder.SetupAllProperties();
            folder.Setup(x => x.Name).Returns(folderName);
            folder.Setup(x => x.Level).Returns(folderLevel);
            folder.Setup(x => x.Id).Returns(folderId);

            UmbracoFactory.Setup(x => x.GetContainer(folderName, folderLevel)).Returns(folder.Object);

            return this;
        }

        public DocumentTypeBuilderSetup ReturnsDataType(CmsDataType dataType)
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
