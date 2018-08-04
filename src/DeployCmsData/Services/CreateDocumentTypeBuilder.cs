//using System;
//using System.Globalization;
//using DeployCmsData.Constants;
//using DeployCmsData.Interfaces;
//using Umbraco.Core;
//using Umbraco.Core.Models;
//using Umbraco.Core.Models.EntityBase;
//using Umbraco.Core.Services;

//namespace DeployCmsData.Services
//{
//    public class CreateDocumentTypeBuilder
//    {
//        private readonly IContentTypeService _contentTypeService;
//        private readonly IUmbracoFactory _factory;

//        public CreateDocumentTypeBuilder(ApplicationContext applicationContext)
//        {
//            if (applicationContext == null)
//                throw new ArgumentNullException(nameof(applicationContext));

//            _contentTypeService = applicationContext.Services.ContentTypeService;
//            _factory = new UmbracoFactory(_contentTypeService);
//        }

//        public CreateDocumentTypeBuilder(IContentTypeService contentTypeService, 
//            IUmbracoFactory factory)
//        {
//            _contentTypeService = contentTypeService;
//            _factory = factory;
//        }

//        public IUmbracoEntity CreateFolderAtRoot(string name)
//        {
//            var container = _factory.GetContainer(name, 1);
//            if (container != null) return container;                          
            
//            var newContainer = _factory.NewContainer(CmsContentValues.RootFolder, name, 1);
//            return newContainer;
//        }

//        public IUmbracoEntity CreateFolder(string name, string parentFolderName, int parentLevel)
//        {
//            var parent = _factory.GetContainer(parentFolderName, parentLevel);
//            if (parent == null)
//                throw new ArgumentException(ExceptionMessages.ParentFolderNotFound, parentFolderName);

//            var container = _factory.NewContainer(parent.Id, name, parentLevel);
//            return container;
//        }

//        public IContentType CreateDocumentTypeAtRoot(
//            string alias,
//            string icon,
//            string name,
//            string description,
//            bool isAllowedAtRoot)
//        {
//            return CreateDocumentType(CmsContentValues.RootFolder, alias, icon, name, description, isAllowedAtRoot);
//        }

//        public DocumentTypeBuilder CreateDocumentTypeAtRoot()
//        {
//            return new DocumentTypeBuilder(_contentTypeService, _factory);
//        }

//        public IContentType CreateDocumentType(
//            string parentAlias,
//            string alias,
//            string icon,
//            string name,
//            string description,
//            bool isAllowedAtRoot)
//        {
//            var parent = _contentTypeService.GetContentType(parentAlias);
//            if (parent == null)
//                throw new ArgumentException(ExceptionMessages.ParentAliasNotFound, parentAlias);

//            return CreateDocumentType(parent.Id, alias, icon, name, description, isAllowedAtRoot);
//        }

//        public IContentType CreateDocumentType(
//            int parentId,
//            string alias,
//            string icon,
//            string name,
//            string description,
//            bool isAllowedAtRoot)
//        {
//            var documentType = _contentTypeService.GetContentType(alias);
//            if (documentType != null) return documentType;

//            documentType = _factory.NewContentType(parentId);
//            if (documentType == null)
//                throw new ArgumentException(ExceptionMessages.CannotCreateDocumentType, parentId.ToString(CultureInfo.InvariantCulture));

//            documentType.Alias = alias;
//            documentType.Icon = icon;
//            documentType.Name = name;
//            documentType.Description = description;
//            documentType.AllowedAsRoot = isAllowedAtRoot;
//            documentType.IsContainer = false;

//            _contentTypeService.Save(documentType);

//            return documentType;
//        }
//    }
//}
