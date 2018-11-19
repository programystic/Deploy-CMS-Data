using DeployCmsData.UmbracoCms.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.Services
{
    public class UmbracoLibrary
    {
        IContentTypeService _contentTypeService;
        IContentService _contentService;
        IUmbracoFactory _umbracoFactory;

        public UmbracoLibrary()
        {
            var applicationContext = UmbracoContext.Current.Application;
            _contentTypeService = applicationContext.Services.ContentTypeService;
            _contentService = applicationContext.Services.ContentService;
            _umbracoFactory = new UmbracoFactory(_contentTypeService);
        }

        public UmbracoLibrary(IContentTypeService contentTypeService, IContentService contentService, IUmbracoFactory umbracoFactory)
        {            
            _contentTypeService = contentTypeService;
            _contentService = contentService;
            _umbracoFactory = umbracoFactory;
        }

        public void DeleteAllDocumentTypes()
        {
            var documentTypes = _contentTypeService.GetAllContentTypes();
            if (documentTypes == null || documentTypes.Count() == 0) return;
            
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
    }
}