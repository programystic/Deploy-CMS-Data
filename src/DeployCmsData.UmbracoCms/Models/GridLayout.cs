using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridLayout
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("areas")]
        public Collection<GridArea> Areas { get; }

        public GridLayout()
        {
            Areas = new Collection<GridArea>();
        }
    }
}