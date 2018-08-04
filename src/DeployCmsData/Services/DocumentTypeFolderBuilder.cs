using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Services
{
    public class DocumentTypeFolderBuilder
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IUmbracoFactory _factory;
        private string _name { get; set; }
        private string _parentFolderName { get; set; }
        private int _parentLevel { get; set; }

        public DocumentTypeFolderBuilder(IContentTypeService contentTypeService, IUmbracoFactory factory)
        {
            _contentTypeService = contentTypeService;
            _factory = factory;
        }

        public DocumentTypeFolderBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public DocumentTypeFolderBuilder ParentFolderName(string name)
        {
            _parentFolderName = name;
            return this;
        }

        public DocumentTypeFolderBuilder ParentLevel(int level)
        {
            _parentLevel = level;
            return this;
        }

        public IUmbracoEntity BuildAtRoot()
        {
            var container = _factory.GetContainer(_name, 1);
            if (container != null) return container;

            var newContainer = _factory.NewContainer(ValueConstants.RootFolder, _name, 1);
            return newContainer;
        }

        public IUmbracoEntity Build()
        {
            var parent = _factory.GetContainer(_parentFolderName, _parentLevel);
            if (parent == null)
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, _parentFolderName);

            var container = _factory.NewContainer(parent.Id, _name, _parentLevel);
            return container;
        }
    }
}
