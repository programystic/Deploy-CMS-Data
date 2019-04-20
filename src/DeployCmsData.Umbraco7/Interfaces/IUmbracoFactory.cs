using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;

namespace DeployCmsData.Umbraco7.Interfaces
{
    public interface IUmbracoFactory
    {
        IContentType NewContentType(int parentId);
        IUmbracoEntity NewContainer(int parentId, string name, int parentLevel);
        IUmbracoEntity GetContainer(string name, int level);
        PropertyType NewPropertyType(IDataTypeDefinition dataTypeDefinition, string propertyAlias);        
        IEnumerable<IContent> GetChildren(IContent content);
        ITemplate NewTemplate(string name, string templateAlias);
        PropertyType GetPropertyType(IContentType dataTypeDefinition, string propertyAlias);
    }
}