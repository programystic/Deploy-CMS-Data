using DeployCmsData.Umbraco7.Enums;
using Newtonsoft.Json;

namespace DeployCmsData.Umbraco7.Models
{
    public class MultiNodeTreePickerStartNodePreValue
    {
        [JsonProperty("type")]
        public StartNodeType StartNodeType { get; set; }
        public string Query { get; set; }
        public string Id { get; set; }
    }
}
