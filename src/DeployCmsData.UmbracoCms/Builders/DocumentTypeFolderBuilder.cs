using System;
using DeployCmsData.Core.Constants;
using DeployCmsData.UmbracoCms.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class DocumentTypeFolderBuilder
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IUmbracoFactory _factory;
        private string _name { get; set; }

        public DocumentTypeFolderBuilder(string name)
        {
            var applicationContext = UmbracoContext.Current.Application;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            _factory = new UmbracoFactory(_contentTypeService);
            _name = name;
        }

        public DocumentTypeFolderBuilder(IContentTypeService contentTypeService, IUmbracoFactory factory, string name)
        {
            _contentTypeService = contentTypeService;
            _factory = factory;
            _name = name;
        }

        public IUmbracoEntity BuildAtRoot()
        {
            var container = _factory.GetContainer(_name, 1);
            if (container != null)
            {
                return container;
            }

            var newContainer = _factory.NewContainer(Constants.Umbraco.RootFolder, _name, 1);
            return newContainer;
        }

        public IUmbracoEntity BuildWithParentFolder(string parentFolderName, int parentFolderLevel)
        {
            var parent = _factory.GetContainer(parentFolderName, parentFolderLevel);
            if (parent == null)
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, parentFolderName);
            }

            var container = _factory.NewContainer(parent.Id, _name, parentFolderLevel);
            return container;
        }

        public IUmbracoEntity BuildWithParentFolder(string parentFolderName)
        {
            var folderLevel = 0;
            IUmbracoEntity parentFolder = null;

            if (string.IsNullOrEmpty(parentFolderName))
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);
            }

            while (folderLevel < Constants.Umbraco.MaximumFolderLevel && parentFolder == null)
            {
                folderLevel++;
                parentFolder = _factory.GetContainer(parentFolderName, folderLevel);
            }

            if (parentFolder == null)
            {
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, parentFolderName);
            }

            var container = _factory.NewContainer(parentFolder.Id, _name, folderLevel);
            return container;
        }
    }
}