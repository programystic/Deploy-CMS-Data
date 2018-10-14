using Umbraco.Core.Models;

namespace DeployCmsData.Services.Interfaces
{
    public interface IContentPropertiesService
    {
        PropertyType GetPropertyType(IContentType contentType, string propertyTypeAlias);
    }
}
