using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.Services
{
    public class DocumentTypeFolderBuilder
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IUmbracoFactory _factory;
        private string _name { get; set; }

        public DocumentTypeFolderBuilder()
        {
            var applicationContext = UmbracoContext.Current.Application;

            var dataTypeService = applicationContext.Services.DataTypeService;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            _factory = new UmbracoFactory(_contentTypeService, dataTypeService);
        }

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

        public IUmbracoEntity BuildAtRoot()
        {
            var container = _factory.GetContainer(_name, 1);
            if (container != null) return container;

            var newContainer = _factory.NewContainer(ValueConstants.RootFolder, _name, 1);
            return newContainer;
        }

        public IUmbracoEntity BuildWithParentFolder(string parentFolderName, int parentFolderLevel)
        {
            var parent = _factory.GetContainer(parentFolderName, parentFolderLevel);
            if (parent == null)
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, parentFolderName);

            var container = _factory.NewContainer(parent.Id, _name, parentFolderLevel);
            return container;
        }

        public IUmbracoEntity BuildWithParentFolder(string parentFolderName)
        {
            var folderLevel = 0;
            IUmbracoEntity parentFolder = null;

            if (string.IsNullOrEmpty(parentFolderName))
                throw new ArgumentException(ExceptionMessages.ParentFolderNameNotDefined);

            while (folderLevel < ValueConstants.MaximumFolderLevel && parentFolder == null)
            {
                folderLevel++;
                parentFolder = _factory.GetContainer(parentFolderName, folderLevel);
            }

            if (parentFolder == null)
                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, parentFolderName);

            var container = _factory.NewContainer(parentFolder.Id, _name, folderLevel);
            return container;
        }
    }
}
