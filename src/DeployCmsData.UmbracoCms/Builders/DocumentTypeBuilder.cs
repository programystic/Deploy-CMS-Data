using DeployCmsData.Core.Constants;
using DeployCmsData.UmbracoCms.Extensions;
using DeployCmsData.UmbracoCms.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web;
using Validation;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class DocumentTypeBuilder
    {
        private IContentTypeService _contentTypeService;
        private IDataTypeService _dataTypeService;
        private IUmbracoFactory _factory;
        private readonly string _alias;
        private string _name;
        private string _icon;
        private string _iconColour;
        private string _description;
        private string _tab;
        private ITemplate _defaultTemplate;
        private Dictionary<string, int> _tabSortOrder = new Dictionary<string, int>();
        private bool? _allowAtRoot;

        internal readonly IList<string> RemoveFieldList = new List<string>();
        internal readonly IList<IContentTypeComposition> Compositions = new List<IContentTypeComposition>();
        internal IList<ContentTypeSort> AllowedChildNodeTypes = new List<ContentTypeSort>();
        internal IList<ContentTypeSort> RemoveAllowedChildNodeTypes = new List<ContentTypeSort>();
        public IList<PropertyBuilder> FieldList { get; } = new List<PropertyBuilder>();

        public DocumentTypeBuilder(string alias)
        {
            Requires.NotNullOrWhiteSpace(alias, nameof(alias));

            var services = UmbracoContext.Current.Application.Services;
            _dataTypeService = services.DataTypeService;
            _contentTypeService = services.ContentTypeService;

            _factory = new UmbracoFactory(_contentTypeService);
            _alias = alias;
        }

        public DocumentTypeBuilder(
            IContentTypeService contentTypeService,
            IUmbracoFactory factory,
            IDataTypeService dataTypeService,
            string alias)
        {
            Requires.NotNull(dataTypeService, nameof(dataTypeService));

            _dataTypeService = Requires.NotNull(dataTypeService, nameof(dataTypeService));
            _contentTypeService = Requires.NotNull(contentTypeService, nameof(contentTypeService));
            _factory = Requires.NotNull(factory, nameof(factory));
            _alias = Requires.NotNull(alias, nameof(alias));
        }

        public IContentType BuildWithParent(string parentAlias)
        {
            var parent = _contentTypeService.GetContentType(parentAlias);
            Verify.Operation(parent != null, ExceptionMessages.ParentAliasNotFound);

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
                parentFolder = new DocumentTypeFolderBuilder(folderName).BuildAtRoot();
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

            SetNewDocumentTypeProperties(documentType, parentId);
            SetupTabs(documentType);
            UpdateFields(documentType);
            RemoveFields(documentType);
            documentType.AllowedContentTypes = AllowedChildNodeTypes;
            AddCompositions(documentType);

            _contentTypeService.Save(documentType);

            return documentType;
        }

        private void AddCompositions(IContentType documentType)
        {
            foreach (var composition in Compositions)
            {
                documentType.AddContentType(composition);
            }
        }

        private void SetupTabs(IContentType documentType)
        {
            foreach (var tab in _tabSortOrder)
            {
                var propertyGroup = documentType.PropertyGroups.FirstOrDefault(x => x.Name == tab.Key);

                if (propertyGroup == null)
                {
                    propertyGroup = new PropertyGroup();
                    documentType.PropertyGroups.Add(propertyGroup);
                }

                propertyGroup.Name = tab.Key;
                propertyGroup.SortOrder = tab.Value;
            }
        }

        public IContentType Update()
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            Verify.Operation(documentType != null, ExceptionMessages.DocumentTypeNotFound + ":" + _alias);

            UpdateAllowedContentTypes(documentType);
            UpdateDocumentTypeProperties(documentType);
            UpdateFields(documentType);
            RemoveFields(documentType);
            SetupTabs(documentType);
            AddCompositions(documentType);

            _contentTypeService.Save(documentType);

            return documentType;
        }

        private void UpdateAllowedContentTypes(IContentType documentType)
        {
            bool updated = false;
            foreach (var item in AllowedChildNodeTypes)
            {
                if (!documentType.AllowedContentTypes.Contains(item))
                {
                    updated = true;
                    break;
                }
            }

            if (updated || RemoveAllowedChildNodeTypes.Count > 0)
            {
                CopyExistingAllowedTypes(documentType);
                RemoveNotAllowedTypes();
                documentType.AllowedContentTypes = AllowedChildNodeTypes;
            }
        }

        private void CopyExistingAllowedTypes(IContentType documentType)
        {
            foreach (var allowedType in documentType.AllowedContentTypes)
            {
                if (!AllowedChildNodeTypes.Any(x => x.Id.Value == allowedType.Id.Value))
                {
                    AllowedChildNodeTypes.Add(allowedType);
                }
            }
        }

        private void RemoveNotAllowedTypes()
        {
            foreach (var allowedType in RemoveAllowedChildNodeTypes)
            {
                if (AllowedChildNodeTypes.Any(x => x.Id.Value == allowedType.Id.Value))
                {
                    AllowedChildNodeTypes.Remove(allowedType);
                }
            }
        }

        private IContentType CreateNewDocumentType(int parentId)
        {
            Verify.Operation(!string.IsNullOrEmpty(_alias), ExceptionMessages.AliasNotDefined);

            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType != null)
            {
                return documentType;
            }

            documentType = _factory.NewContentType(parentId);
            Verify.Operation(documentType != null, ExceptionMessages.CannotCreateDocumentType);

            return documentType;
        }

        private void SetNewDocumentTypeProperties(IContentType documentType, int parentId)
        {
            documentType.Alias = _alias;

            documentType.Icon =
                (string.IsNullOrEmpty(_icon) ? Constants.Icons.RoadSign : _icon)
                + (!string.IsNullOrEmpty(_iconColour) ? " " + _iconColour : "");

            documentType.Name = string.IsNullOrEmpty(_name) ? _alias.AliasToName() : _name;
            documentType.Description = _description;
            documentType.AllowedAsRoot = _allowAtRoot.HasValue ? _allowAtRoot.Value : false;
            documentType.IsContainer = false;

            if (_defaultTemplate != null)
            {
                documentType.SetDefaultTemplate(_defaultTemplate);
            }
        }

        private void UpdateDocumentTypeProperties(IContentType documentType)
        {
            documentType.Alias = !string.IsNullOrWhiteSpace(_alias) ? _alias : documentType.Alias;
            documentType.Icon = !string.IsNullOrEmpty(_icon) ? _icon : documentType.Icon;
            documentType.Name = !string.IsNullOrEmpty(_name) ? _name : documentType.Name;
            documentType.Description = !string.IsNullOrEmpty(_description) ? _description : documentType.Description;
            documentType.AllowedAsRoot = _allowAtRoot.HasValue ? _allowAtRoot.Value : documentType.AllowedAsRoot;

            if (_defaultTemplate != null)
            {
                documentType.SetDefaultTemplate(_defaultTemplate);
            }
        }

        private void UpdateFields(IContentType documentType)
        {
            foreach (var field in FieldList)
            {
                if (documentType.PropertyTypeExists(field.AliasValue))
                {
                    var propertyType = _factory.GetPropertyType(documentType, field.AliasValue);
                    UpdateField(documentType, field, propertyType);
                }
                else
                {
                    AddNewField(documentType, field);
                }
            }
        }

        private void RemoveFields(IContentType documentType)
        {
            foreach (var alias in RemoveFieldList)
            {
                if (documentType.PropertyTypeExists(alias))
                {
                    documentType.RemovePropertyType(alias);
                }
            }
        }

        private void UpdateField(IContentType documentType, PropertyBuilder field, PropertyType propertyType)
        {
            propertyType.Name = field.NameValue ?? propertyType.Name;
            propertyType.Description = field.DescriptionValue ?? propertyType.Description;
            propertyType.ValidationRegExp = field.RegularExpressionValue ?? propertyType.ValidationRegExp;
            propertyType.Mandatory = field.MandatoryValue ?? propertyType.Mandatory;

            if (field.DataTypeValue != Guid.Empty)
            {
                var dataTypeDefinition = _dataTypeService.GetDataTypeDefinitionById(field.DataTypeValue);
                Verify.Operation(dataTypeDefinition != null, ExceptionMessages.CannotFindDataType + field.DataTypeValue);

                propertyType.DataTypeDefinitionId = dataTypeDefinition.Id;
            }

            if (field.TabValue != null)
            {
                documentType.MovePropertyType(field.AliasValue, field.TabValue);
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
            Verify.Operation(dataTypeDefinition != null, ExceptionMessages.CannotFindDataType + field.DataTypeValue);

            propertyType = _factory.NewPropertyType(dataTypeDefinition, field.AliasValue);
            propertyType.Name = field.NameValue;
            propertyType.Description = field.DescriptionValue;
            propertyType.ValidationRegExp = field.RegularExpressionValue;
            propertyType.Mandatory = field.MandatoryValue.HasValue ? field.MandatoryValue.Value : false;

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
                field.DataTypeValue = Constants.DataType.TextString;
            }

            if (string.IsNullOrEmpty(field.NameValue))
            {
                field.NameValue = field.AliasValue.AliasToName();
            }

            if (string.IsNullOrEmpty(field.TabValue))
            {
                field.TabValue = _tab;
            }
        }

        public void DeleteDocumentType()
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            Verify.Operation(documentType != null, ExceptionMessages.DocumentTypeNotFound + " : " + _alias);

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

        public DocumentTypeBuilder Icon(string documentTypeIcon, string iconColour)
        {
            _icon = documentTypeIcon;
            _iconColour = iconColour;
            return this;
        }

        public DocumentTypeBuilder Description(string documentTypeDescription)
        {
            _description = documentTypeDescription;
            return this;
        }

        public DocumentTypeBuilder AllowedAsRoot()
        {
            _allowAtRoot = true;
            return this;
        }

        public DocumentTypeBuilder NoAllowedAsRoot()
        {
            _allowAtRoot = false;
            return this;
        }

        public DocumentTypeBuilder AddAllowedChildNodeType(string alias)
        {
            var documentType = _contentTypeService.GetContentType(alias);

            if (documentType != null)
            {
                var newItem = new ContentTypeSort(documentType.Id, AllowedChildNodeTypes.Count + 1);
                if (!AllowedChildNodeTypes.Contains(newItem))
                {
                    AllowedChildNodeTypes.Add(new ContentTypeSort(documentType.Id, AllowedChildNodeTypes.Count + 1));
                }
            }

            return this;
        }

        public DocumentTypeBuilder RemoveAllowedChildNodeType(string alias)
        {
            var documentType = _contentTypeService.GetContentType(alias);
            if (documentType != null)
            {
                RemoveAllowedChildNodeTypes.Add(new ContentTypeSort(documentType.Id, RemoveAllowedChildNodeTypes.Count + 1));
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

            FieldList.Add(fieldBuilder);
            return fieldBuilder;
        }

        public DocumentTypeBuilder RemoveField(string alias)
        {
            RemoveFieldList.Add(alias);

            return this;
        }

        public DocumentTypeBuilder DefaultTab(string tab)
        {
            _tab = tab;
            return this;
        }

        public DocumentTypeBuilder DefaultTemplate(ITemplate template)
        {
            _defaultTemplate = template;

            return this;
        }

        public bool AllowedContentTypesAreEqual(IEnumerable<ContentTypeSort> toCompare)
        {
            return AllowedChildNodeTypes.Equals(toCompare);
        }

        public int AllowedChildNodeTypesCount()
        {
            return AllowedChildNodeTypes.Count;
        }

        public DocumentTypeBuilder TabSortOrder(string tab, int sortOrder)
        {
            if (_tabSortOrder.ContainsKey(tab))
            {
                _tabSortOrder[tab] = sortOrder;
            }
            else
            {
                _tabSortOrder.Add(tab, sortOrder);
            }

            return this;
        }
    }
}