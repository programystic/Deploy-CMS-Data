//using System;
//using System.Linq;
//using DeployCmsData.Interfaces;
//using Umbraco.Core.Models;

//namespace DeployCmsData.Services
//{
//    public sealed class ContentPropertiesService : IContentPropertiesService
//    {
//        public PropertyType GetPropertyType(IContentType contentType, string alias)
//        {
//            if (contentType == null) throw new ArgumentNullException();
//            return contentType.PropertyTypes.FirstOrDefault(t => t.Alias == alias);
//        }
//    }
//}
