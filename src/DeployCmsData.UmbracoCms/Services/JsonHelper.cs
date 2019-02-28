using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DeployCmsData.UmbracoCms.Services
{
    internal static class JsonHelper
    {
        public static string SerializePreValueObject(object value)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = true } },
                NullValueHandling = NullValueHandling.Ignore,

            };

            var jsonString = JsonConvert.SerializeObject(value, serializerSettings);

            return jsonString;
        }
    }
}