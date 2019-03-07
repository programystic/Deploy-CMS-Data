using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Services
{
    public class UmbracoLibrary
    {
        private IContentTypeService _contentTypeService;
        private IContentService _contentService;
        private IDataTypeService _dataTypeService;
        private IFileService _fileService;

        public UmbracoLibrary()
        {
            var services = Current.Services;
            _contentTypeService = services.ContentTypeService;
            _contentService = services.ContentService;
            _dataTypeService = services.DataTypeService;
            _fileService = services.FileService;
        }

        public UmbracoLibrary(IContentTypeService contentTypeService,
            IContentService contentService)
        {
            _contentTypeService = contentTypeService;
            _contentService = contentService;
        }

        public void DeleteAllDocumentTypes()
        {
            var documentTypes = _contentTypeService.GetAll();
            if (documentTypes == null || documentTypes.Count() == 0)
            {
                return;
            }

            _contentTypeService.Delete(documentTypes);
        }

        public void DeleteAllDocumentTypeFolders()
        {
            var folders = _contentTypeService.GetContainers(new int[0]);

            foreach (var folder in folders)
            {
                _contentTypeService.DeleteContainer(folder.Id);
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
                //DeleteAllContent(_umbracoFactory.GetChildren(item));
                _contentService.Delete(item);
            }
        }

        public void DeleteDataTypeById(Guid id)
        {
            var dataType = _dataTypeService.GetDataType(id);
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