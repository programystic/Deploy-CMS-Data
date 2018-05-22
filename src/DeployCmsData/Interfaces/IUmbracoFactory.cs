using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;

namespace DeployCmsData.Interfaces
{
    public interface IUmbracoFactory
    {
        IContentType NewContentType(int parentId);
        IUmbracoEntity NewContainer(int parentId, string name, int parentLevel);
        IUmbracoEntity GetContainer(string name, int level);
        PropertyType NewPropertyType(IDataTypeDefinition dataTypeDefinition, string propertyAlias);
    }
}