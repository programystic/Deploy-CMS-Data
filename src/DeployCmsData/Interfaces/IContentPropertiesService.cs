using Umbraco.Core.Models;

namespace DeployCmsData.Interfaces
{
    public interface IContentPropertiesService
    {
        PropertyType GetPropertyType(IContentType contentType, string propertyTypeAlias);
    }
}
