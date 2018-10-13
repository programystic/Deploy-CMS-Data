using System.Linq;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.Services
{
    public class CmsLibrary
    {
        IContentTypeService _contentTypeService;

        public CmsLibrary()
        {
            var applicationContext = UmbracoContext.Current.Application;
            _contentTypeService = applicationContext.Services.ContentTypeService;
        }

        public CmsLibrary(IContentTypeService contentTypeService)
        {            
            _contentTypeService = contentTypeService;
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
    }
}