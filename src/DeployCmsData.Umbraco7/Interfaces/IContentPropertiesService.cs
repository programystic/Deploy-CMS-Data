using Umbraco.Core.Models;

namespace DeployCmsData.Umbraco7.Interfaces
{
    public interface IContentPropertiesService
    {
        PropertyType GetPropertyType(IContentType contentType, string propertyTypeAlias);
    }
}
