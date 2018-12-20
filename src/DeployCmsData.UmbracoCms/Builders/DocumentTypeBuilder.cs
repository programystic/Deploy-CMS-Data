using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
        private string _tab;
        public readonly IList<PropertyBuilder> AddFieldList = new List<PropertyBuilder>();
        internal readonly IList<PropertyBuilder> UpdateFieldList = new List<PropertyBuilder>();
        internal readonly IList<PropertyBuilder> RemoveFieldList = new List<PropertyBuilder>();
        internal readonly IList<ContentTypeSort> AllowedChildNodeTypes = new List<ContentTypeSort>();
        internal readonly IList<IContentTypeComposition> Compositions = new List<IContentTypeComposition>();

        public DocumentTypeBuilder(string alias)
        {
            var applicationContext = UmbracoContext.Current.Application;

            _dataTypeService = applicationContext.Services.DataTypeService;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            _factory = new UmbracoFactory(_contentTypeService);
            _alias = alias;
        }

        public DocumentTypeBuilder(
            IContentTypeService contentTypeService,
            IUmbracoFactory factory,
            IDataTypeService dataTypeService,
            string alias)
        {
            _dataTypeService = dataTypeService;
            _contentTypeService = contentTypeService;
            _factory = factory;
            _alias = alias;
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
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined, nameof(folderName));
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

        public IContentType ReBuild()
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType == null)
            {
                throw new ArgumentException(ExceptionMessages.DocumentTypeNotFound + ":" + _alias);
            }

            AddNewFields(documentType);
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
            documentType.Icon = string.IsNullOrEmpty(_icon) ? Constants.Icons.RoadSign : _icon;
            documentType.Name = string.IsNullOrEmpty(_name) ? AliasToName(_alias) : _name;
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

                propertyType = AddNewField(documentType, field);
            }
        }

        private PropertyType AddNewField(IContentType documentType, PropertyBuilder field)
        {
            PropertyType propertyType;
            IDataTypeDefinition dataTypeDefinition = null;

            propertyType = documentType.PropertyTypes.FirstOrDefault(x => x.Alias == field.AliasValue);
            if (propertyType != null)
            {
                return propertyType;
            }

            SetDefaultFieldValues(field);

            dataTypeDefinition = _dataTypeService.GetDataTypeDefinitionById(field.DataTypeValue);

            if (dataTypeDefinition == null)
            {
                throw new ArgumentException(ExceptionMessages.CannotFindDataType + field.DataTypeValue);
            }

            propertyType = _factory.NewPropertyType(dataTypeDefinition, field.AliasValue);
            propertyType.Name = field.NameValue;
            propertyType.Description = field.DescriptionValue;
            propertyType.ValidationRegExp = field.RegularExpressionValue;
            propertyType.Mandatory = field.MandatoryValue;

            if (string.IsNullOrEmpty(field.TabValue))
            {
                documentType.AddPropertyType(propertyType);
            }
            else
            {
                documentType.AddPropertyType(propertyType, field.TabValue);
            }

            return propertyType;
        }

        private void SetDefaultFieldValues(PropertyBuilder field)
        {
            if (field.DataTypeValue == null || field.DataTypeValue == Guid.Empty)
            {
                field.DataTypeValue = Constants.DataType.Textstring;
            }

            if (string.IsNullOrEmpty(field.NameValue))
            {
                field.NameValue = AliasToName(field.AliasValue);
            }

            if (string.IsNullOrEmpty(field.TabValue))
            {
                field.TabValue = _tab;
            }
        }

        private static string AliasToName(string value)
        {
            return Regex.Replace(value, "(\\B[A-Z])", " $1").ToFirstUpperInvariant();
        }

        public void DeleteDocumentType()
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType == null)
            {
                throw new ArgumentException(ExceptionMessages.DocumentTypeNotFound + ":" + _alias);
            }

            _contentTypeService.Delete(documentType);
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

        public PropertyBuilder AddField(string alias)
        {
            var fieldBuilder = new PropertyBuilder(alias);

            AddFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public PropertyBuilder RemoveField(string alias)
        {
            var fieldBuilder = new PropertyBuilder(alias);
            RemoveFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public PropertyBuilder UpdateField(string alias)
        {
            var fieldBuilder = new PropertyBuilder(alias);
            UpdateFieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public DocumentTypeBuilder DefaultTab(string tab)
        {
            _tab = tab;
            return this;
        }
    }
}