using DeployCmsData.UmbracoCms.Enums;
using Newtonsoft.Json;

namespace DeployCmsData.UmbracoCms.Models
{
    public class MultiNodeTreePickerStartNodePreValue
    {
        [JsonProperty("type")]
        public StartNodeType StartNodeType { get; set; }
        public string Query { get; set; }
        public string Id { get; set; }
    }
}
