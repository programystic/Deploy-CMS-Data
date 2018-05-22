using System;
using System.Collections.Generic;
using System.Globalization;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using Umbraco.Core.Models;
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
        private string _helpText;
        private bool _allowedAtRoot = false;
        private int _parentId;
        internal readonly IList<FieldBuilder> FieldList;

        public DocumentTypeBuilder(IContentTypeService contentTypeService, IUmbracoFactory factory)
        {
            _contentTypeService = contentTypeService;
            _factory = factory;
            FieldList = new List<FieldBuilder>();
        }

        public IContentType Build()
        {
            var documentType = _contentTypeService.GetContentType(_alias);
            if (documentType != null) return documentType;

            documentType = _factory.NewContentType(_parentId);
            if (documentType == null)
                throw new ArgumentException(ExceptionMessages.CannotCreateDocumentType, _parentId.ToString(CultureInfo.InvariantCulture));

            documentType.Alias = _alias;
            documentType.Icon = _icon;
            documentType.Name = _name;
            documentType.Description = _description;
            documentType.AllowedAsRoot = _allowedAtRoot;
            documentType.IsContainer = false;

            foreach (var field in FieldList)
            {
                // Add / Update fields
            }

            _contentTypeService.Save(documentType);
            return documentType;
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

        public DocumentTypeBuilder HelpText(string helpText)
        {
            _helpText = helpText;
            return this;
        }

        public DocumentTypeBuilder AllowedAtRoot()
        {
            _allowedAtRoot = true;
            return this;
        }

        public DocumentTypeBuilder ParentId(int parentId)
        {
            _parentId = parentId;
            return this;
        }

        public FieldBuilder AddField(string alias, string tab, string type)
        {
            return new FieldBuilder(alias, tab, type, this);
        }

        public FieldBuilder UpdateField(string alias, string tab, string type)
        {
            return new FieldBuilder(alias, tab, type, this);
        }
    }
}