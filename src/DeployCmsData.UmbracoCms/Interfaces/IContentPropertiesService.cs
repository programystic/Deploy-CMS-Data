using Umbraco.Core.Models;

namespace DeployCmsData.UmbracoCms.Interfaces
{
    public interface IContentPropertiesService
    {
        PropertyType GetPropertyType(IContentType contentType, string propertyTypeAlias);
    }
}
