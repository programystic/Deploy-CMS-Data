using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DeployCmsData.UmbracoCms.Services
{
    static class JsonHelper
    {
        public static string SerializePreValueObject(object value)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(value, serializerSettings);
        }
    }
}
