using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DeployCmsData.Core.Constants;
using DeployCmsData.UmbracoCms.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class DocumentTypeBuilder
    {
        private IContentTypeService _contentTypeService;
        private IDataTypeService _dataTypeService;
        private IUmbracoFactory _factory;
        private string _alias;
        private string _name;
        private string _icon;
        private string _description;
        internal readonly IList<PropertyBuilder> AddFieldList = new List<PropertyBuilder>();
        internal readonly IList<PropertyBuilder> UpdateFieldList = new List<PropertyBuilder>();
        internal readonly IList<PropertyBuilder> RemoveFieldList = new List<PropertyBuilder>();
        internal readonly IList<ContentTypeSort> AllowedChildNodeTypes = new List<ContentTypeSort>();
        internal readonly IList<IContentTypeComposition> Compositions = new List<IContentTypeComposition>();
        //internal readonly IList<string> TabListAdd = new List<string>();
        //internal readonly IList<string> TabListRemove = new List<string>();

        public DocumentTypeBuilder()
        {
            Initialise(UmbracoContext.Current.Application);
        }

        public DocumentTypeBuilder(ApplicationContext applicationContext)
        {
            Initialise(applicationContext);
        }

        private void Initialise(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
            {
                throw new ArgumentNullException(nameof(applicationContext));
            }

            _dataTypeService = applicationContext.Services.DataTypeService;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            //_factory = new UmbracoFactory(_contentTypeService, _dataTypeService);
            _factory = new UmbracoFactory(_contentTypeService);
        }

        public DocumentTypeBuilder(
            IContentTypeService contentTypeService,
            IUmbracoFactory factory,
            IDataTypeService dataTypeService)
        {
            _dataTypeService = dataTypeService;
            _contentTypeService = contentTypeService;
            _factory = factory;
        }

        public IContentType BuildWithParent(string parentAlias)
        {
            var parent = _contentTypeService.GetContentType(parentAlias);
            if (parent == null)
            {
                throw new ArgumentException(ExceptionMessages.ParentAliasNotFound, parentAlias);
            }

            return BuildDocumentType(parent.Id);
        }

        public IContentType BuildInFolder(string folderName, int folderLevel)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);
            }

            var parentFolder = _factory.GetContainer(folderName, folderLevel);
            if (parentFolder == null)
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, folderName);
            }

            return BuildDocumentType(parentFolder.Id);
        }

        public IContentType BuildInFolder(string folderName)
        {
            var folderLevel = 1;
            IUmbracoEntity parentFolder = null;

            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);
            }

            while (folderLevel <= Constants.Umbraco.MaximumFolderLevel && parentFolder == null)
            {
                parentFolder = _factory.GetContainer(folderName, folderLevel);
                folderLevel++;
            }

            if (parentFolder == null)
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, folderName);
            }

            return BuildDocumentType(parentFolder.Id);
        }

        public IContentType BuildAtRoot()
        {
            return BuildDocumentType(Constants.Umbraco.RootFolder);
        }

        private IContentType BuildDocumentType(int parentId)
        {
            var documentType = CreateNewDocumentType(parentId);
            SetDocumentTypeProperties(documentType, parentId);
            AddNewFields(documentType);
            documentType.AllowedContentTypes = AllowedChildNodeTypes;

            foreach (var composition in Compositions)
            {
                documentType.AddContentType(composition);
            }

            _contentTypeService.Save(documentType);

            return documentType;
        }

        private IContentType CreateNewDocumentType(int parentId)
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType != null)
            {
                return documentType;
            }

            documentType = _factory.NewContentType(parentId);
            if (documentType == null)
            {
                throw new ArgumentException(ExceptionMessages.CannotCreateDocumentType, parentId.ToString(CultureInfo.InvariantCulture));
            }

            if (string.IsNullOrEmpty(_alias))
            {
                throw new ArgumentException(ExceptionMessages.AliasNotDefined);
            }

            return documentType;
        }

        private void SetDocumentTypeProperties(IContentType documentType, int parentId)
        {
            documentType.Alias = _alias;
            documentType.Icon = _icon;
            documentType.Name = _name;
            documentType.Description = _description;
            documentType.AllowedAsRoot = (parentId == Constants.Umbraco.RootFolder);
            documentType.IsContainer = false;
        }

        private void AddNewFields(IContentType documentType)
        {
            foreach (var field in AddFieldList)
            {
                var propertyType = documentType.PropertyTypes.FirstOrDefault(x => x.Alias == field.AliasValue);
                if (propertyType != null)
                {
                    continue;
                }

                IDataTypeDefinition dataTypeDefinition = null;

                if (!string.IsNullOrEmpty(field.DataTypeValue))
                {
                    dataTypeDefinition = _dataTypeService.GetDataTypeDefinitionByName(field.DataTypeValue);
                }

                if (dataTypeDefinition == null)
                {
                    throw new ArgumentException(ExceptionMessages.CannotFindDataType + field.DataTypeValue);
                }

                propertyType = _factory.NewPropertyType(dataTypeDefinition, field.AliasValue);

                propertyType.Name = field.NameValue;
                propertyType.Description = field.DescriptionValue;
                propertyType.ValidationRegExp = field.RegularExpressionValue;
                propertyType.Mandatory = field.MandatoryValue;

                documentType.AddPropertyType(propertyType, field.TabValue);
            }
        }

        public void DeleteDocumentType(string alias)
        {
            var documentType = _contentTypeService.GetContentType(alias);
            if (documentType == null)
            {
                throw new ArgumentException(ExceptionMessages.DocumentTypeNotFound + ":" + alias);
            }

            _contentTypeService.Delete(documentType);
        }

        public DocumentTypeBuilder Alias(string documentTypeAlias)
        {
            _alias = documentTypeAlias;
            return this;
        }

        public DocumentTypeBuilder Name(string documentTypeName)
        {
            _name = documentTypeName;
            return this;
        }

        public DocumentTypeBuilder Icon(string documentTypeIcon)
        {
            _icon = documentTypeIcon;
            return this;
        }

        public DocumentTypeBuilder Description(string documentTypeDescription)
        {
            _description = documentTypeDescription;
            return this;
        }

        public DocumentTypeBuilder AddAllowedChildNodeType(string alias)
        {
            var documentType = _contentTypeService.GetContentType(alias);
            if (documentType != null)
            {
                AllowedChildNodeTypes.Add(new ContentTypeSort(documentType.Id, AllowedChildNodeTypes.Count + 1));
            }

            return this;
        }

        public DocumentTypeBuilder AddComposition(string alias)
        {
            var documentType = _contentTypeService.GetContentType(alias);
            if (documentType != null)
            {
                Compositions.Add(documentType);
            }

            return this;
        }

        public PropertyBuilder AddField()
        {
            var fieldBuilder = new PropertyBuilder();
            AddFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public PropertyBuilder RemoveField()
        {
            var fieldBuilder = new PropertyBuilder();
            RemoveFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public PropertyBuilder UpdateField()
        {
            var fieldBuilder = new PropertyBuilder();
            UpdateFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }
    }
}