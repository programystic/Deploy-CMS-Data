using System;
using System.Collections.Generic;
using System.Globalization;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Services
{
    public class DocumentTypeBuilder
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IUmbracoFactory _factory;
        private string _alias;
        private string _name;
        private string _icon;
        private string _description;
        private string _parentAlias;

        //internal readonly IList<FieldBuilder> FieldList;

        public DocumentTypeBuilder(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
                throw new ArgumentNullException(nameof(applicationContext));

            _contentTypeService = applicationContext.Services.ContentTypeService;
            _factory = new UmbracoFactory(_contentTypeService);
        }

        public DocumentTypeBuilder(IContentTypeService contentTypeService, IUmbracoFactory factory)
        {
            _contentTypeService = contentTypeService;
            _factory = factory;
            //FieldList = new List<FieldBuilder>();
        }

        public IContentType Build()
        {
            var parent = _contentTypeService.GetContentType(_parentAlias);
            if (parent == null)
                throw new ArgumentException(ExceptionMessages.ParentAliasNotFound, _parentAlias);

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
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType != null) return documentType;

            documentType = _factory.NewContentType(parentId);
            if (documentType == null)
                throw new ArgumentException(ExceptionMessages.CannotCreateDocumentType, parentId.ToString(CultureInfo.InvariantCulture));

            if (string.IsNullOrEmpty(_alias))
                throw new ArgumentException(ExceptionMessages.AliasNotDefined);

            documentType.Alias = _alias;
            documentType.Icon = _icon;
            documentType.Name = _name;
            documentType.Description = _description;
            documentType.AllowedAsRoot = (parentId == ValueConstants.RootFolder);
            documentType.IsContainer = false;

            //foreach (var field in FieldList)
            //{
            //    // Add / Update fields
            //}

            _contentTypeService.Save(documentType);
            return documentType;
        }

        public DocumentTypeBuilder Alias(string alias)
        {
            _alias = alias;
            return this;
        }

        public DocumentTypeBuilder ParentAlias(string alias)
        {
            _parentAlias = alias;
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

        //public FieldBuilder AddField(string alias, string tab, string type)
        //{
        //    return new FieldBuilder(alias, tab, type, this);
        //}

        //public FieldBuilder UpdateField(string alias, string tab, string type)
        //{
        //    return new FieldBuilder(alias, tab, type, this);
        //}
    }
}