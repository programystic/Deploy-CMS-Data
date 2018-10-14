using DeployCmsData.Services.Interfaces;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace DeployCmsData.Services
{
    internal class UmbracoFactory : IUmbracoFactory
    {
        private readonly IContentTypeService _contentTypeService;
        //private readonly IDataTypeService _dataTypeService;

        //public UmbracoFactory(IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        //{
        //    _contentTypeService = contentTypeService;
        //    _dataTypeService = dataTypeService;
        //}

        public UmbracoFactory(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;        
        }

        public IUmbracoEntity NewContainer(int parentId, string name, int parentLevel)
        {
            var container = GetContainer(name, parentLevel + 1);
            if (container != null) return container;

            var result = _contentTypeService.CreateContentTypeContainer(parentId, name);
            if (!result.Success) return null;
            
            var newContainer = result.Result.Entity;
            _contentTypeService.SaveContentTypeContainer(newContainer);
                        
            return newContainer;            
        }

        public IContentType NewContentType(int parentId)
        {
            return new ContentType(parentId);
        }

        public IUmbracoEntity GetContainer(string name, int level)
        {
            var containers = _contentTypeService.GetContentTypeContainers(name, level);
            var container = containers.FirstOrDefault();

            return container;
        }

        public PropertyType NewPropertyType(IDataTypeDefinition dataTypeDefinition, string propertyAlias)
        {                        
            return new PropertyType(dataTypeDefinition, propertyAlias);
        }

        //public PropertyType NewPropertyType(CmsDataType dataTypeName, string propertyAlias)
        //{            
        //    var dataType = _dataTypeService.GetDataTypeDefinitionByName(dataTypeName.ToString());

        //    return new PropertyType(dataType, propertyAlias);
        //}
    }
}
