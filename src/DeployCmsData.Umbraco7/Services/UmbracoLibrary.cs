using DeployCmsData.Umbraco7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.Umbraco7.Services
{
    public class UmbracoLibrary
    {
        private IContentTypeService _contentTypeService;
        private IContentService _contentService;
        private IUmbracoFactory _umbracoFactory;
        private IDataTypeService _dataTypeService;
        private IFileService _fileService;

        public UmbracoLibrary()
        {
            var applicationContext = UmbracoContext.Current.Application;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            _contentService = applicationContext.Services.ContentService;
            _umbracoFactory = new UmbracoFactory(_contentTypeService);
            _dataTypeService = applicationContext.Services.DataTypeService;
            _fileService = applicationContext.Services.FileService;
        }

        public UmbracoLibrary(IContentTypeService contentTypeService, 
            IContentService contentService, 
            IUmbracoFactory umbracoFactory)
        {
            _contentTypeService = contentTypeService;
            _contentService = contentService;
            _umbracoFactory = umbracoFactory;
        }

        public void DeleteAllDocumentTypes()
        {
            var documentTypes = _contentTypeService.GetAllContentTypes();
            if (documentTypes == null || documentTypes.Count() == 0)
            {
                return;
            }

            _contentTypeService.Delete(documentTypes);
        }

        public void DeleteAllDocumentTypeFolders()
        {
            var folders = _contentTypeService.GetContentTypeContainers(new int[0]);

            foreach (var folder in folders)
            {
                _contentTypeService.DeleteContentTypeContainer(folder.Id);
            }

        }

        public void DeleteAllContent()
        {
            DeleteAllContent(_contentService.GetRootContent());
        }

        private void DeleteAllContent(IEnumerable<IContent> content)
        {
            foreach (var item in content)
            {
                DeleteAllContent(_umbracoFactory.GetChildren(item));
                _contentService.Delete(item);
            }
        }

        public void DeleteDataTypeById(Guid id)
        {
            var dataType = _dataTypeService.GetDataTypeDefinitionById(id);
            if (dataType != null)
            {
                _dataTypeService.Delete(dataType);
            }
        }

        public void DeleteAllTemplates()
        {
            DeleteTemplateAndChildren(-1);
        }

        private void DeleteTemplateAndChildren(int templateId)
        {
            var templates = _fileService.GetTemplateChildren(templateId);
            foreach (var template in templates)
            {
                DeleteTemplateAndChildren(template.Id);
                _fileService.DeleteTemplate(template.Alias);
            }
        }
    }
}