using DeployCmsData.UmbracoCms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Entities;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.UmbracoCms.Services
{
    internal class UmbracoFactory : IUmbracoFactory
    {
        private readonly IContentTypeService _contentTypeService;

        public UmbracoFactory(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public IUmbracoEntity NewContainer(int parentId, string name, int parentLevel)
        {
            var container = GetContainer(name, parentLevel + 1);
            if (container != null)
            {
                return container;
            }

            var result = _contentTypeService.CreateContainer(parentId, name);
            if (!result.Success)
            {
                return null;
            }

            var newContainer = result.Result.Entity;
            _contentTypeService.SaveContainer(newContainer);

            return newContainer;
        }

        public IContentType NewContentType(int parentId)
        {
            return new ContentType(parentId);
        }

        public IUmbracoEntity GetContainer(string name, int level)
        {
            var containers = _contentTypeService.GetContainers(name, level);
            var container = containers.FirstOrDefault();

            return container;
        }

        public PropertyType NewPropertyType(IDataType dataTypeDefinition, string propertyAlias)
        {
            return new PropertyType(dataTypeDefinition, propertyAlias);
        }

        public IEnumerable<IContent> GetChildren(IContent content)
        {
            return content.Children();
        }

        public ITemplate NewTemplate(string name, string templateAlias)
        {
            return new Template(name, templateAlias);
        }
    }
}
