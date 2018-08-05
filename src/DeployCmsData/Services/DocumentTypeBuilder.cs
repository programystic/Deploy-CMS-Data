using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.Services
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
        internal readonly IList<FieldBuilder> AddFieldList = new List<FieldBuilder>();
        internal readonly IList<FieldBuilder> UpdateFieldList = new List<FieldBuilder>();
        internal readonly IList<FieldBuilder> RemoveFieldList = new List<FieldBuilder>();
        internal readonly IList<string> TabListAdd = new List<string>();
        internal readonly IList<string> TabListRemove = new List<string>();

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
                throw new ArgumentNullException(nameof(applicationContext));

            _dataTypeService = applicationContext.Services.DataTypeService;
            _contentTypeService = applicationContext.Services.ContentTypeService;            
            _factory = new UmbracoFactory(_contentTypeService, _dataTypeService);
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
                throw new ArgumentException(ExceptionMessages.ParentAliasNotFound, parentAlias);

            return BuildDocumentType(parent.Id);
        }

        public IContentType BuildInFolder(string folderName, int folderLevel)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);

            var parentFolder = _factory.GetContainer(folderName, folderLevel);
            if (parentFolder == null)
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, folderName);

            return BuildDocumentType(parentFolder.Id);
        }

        public IContentType BuildInFolder(string folderName)
        {            
            var folderLevel = 1;
            IUmbracoEntity parentFolder = null;

            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);

            while (folderLevel <= ValueConstants.MaximumFolderLevel && parentFolder == null)
            {
                parentFolder = _factory.GetContainer(folderName, folderLevel);
                folderLevel++;
            }
            
            if (parentFolder == null)
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, folderName);

            return BuildDocumentType(parentFolder.Id);
        }
        
        public IContentType BuildAtRoot()
        {
            return BuildDocumentType(ValueConstants.RootFolder);
        }

        private IContentType BuildDocumentType(int parentId)
        {
            var documentType = CreateDocumentType(parentId);
            SetDocumentTypeProperties(documentType, parentId);
            AddNewFields(documentType);
            _contentTypeService.Save(documentType);

            return documentType;
        }

        private IContentType CreateDocumentType(int parentId)
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType != null) return documentType;

            documentType = _factory.NewContentType(parentId);
            if (documentType == null)
                throw new ArgumentException(ExceptionMessages.CannotCreateDocumentType, parentId.ToString(CultureInfo.InvariantCulture));

            if (string.IsNullOrEmpty(_alias))
                throw new ArgumentException(ExceptionMessages.AliasNotDefined);

            return documentType;
        }

        private void SetDocumentTypeProperties(IContentType documentType, int parentId)
        {
            documentType.Alias = _alias;
            documentType.Icon = _icon;
            documentType.Name = _name;
            documentType.Description = _description;
            documentType.AllowedAsRoot = (parentId == ValueConstants.RootFolder);
            documentType.IsContainer = false;
        }

        private void AddNewFields(IContentType documentType)
        {
            foreach (var field in AddFieldList)
            {
                var propertyType = documentType.PropertyTypes.FirstOrDefault(x => x.Alias == field.AliasValue);
                if (propertyType != null) continue;

                var dataType = _dataTypeService.GetDataTypeDefinitionByName(field.DataTypeValue);
                if (dataType == null)
                    throw new ArgumentException(ExceptionMessages.CantFindDataType + field.DataTypeValue);
                
                propertyType = _factory.NewPropertyType(dataType, field.AliasValue);

                propertyType.Name = field.NameValue;
                propertyType.Description = field.DescriptionValue;
                propertyType.ValidationRegExp = field.RegularExpressionValue;
                propertyType.Mandatory = field.MandatoryValue;

                documentType.AddPropertyType(propertyType, field.TabValue);
            }
        }

        public DocumentTypeBuilder Alias(string alias)
        {
            _alias = alias;
            return this;
        }

        public DocumentTypeBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public DocumentTypeBuilder Icon(string icon)
        {
            _icon = icon;
            return this;
        }

        public DocumentTypeBuilder Description(string description)
        {
            _description = description;
            return this;
        }

        public DocumentTypeBuilder AddTab(string tab)
        {
            TabListAdd.Add(tab);
            return this;
        }

        public DocumentTypeBuilder RemoveTab(string tab)
        {
            TabListRemove.Add(tab);
            return this;
        }

        public FieldBuilder AddField()
        {
            var fieldBuilder = new FieldBuilder();
            AddFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public FieldBuilder RemoveField()
        {
            var fieldBuilder = new FieldBuilder();
            RemoveFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public FieldBuilder UpdateField()
        {
            var fieldBuilder = new FieldBuilder();
            UpdateFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }
    }
}