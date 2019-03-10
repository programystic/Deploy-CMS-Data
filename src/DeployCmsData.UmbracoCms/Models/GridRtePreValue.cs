using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridRtePreValue
    {
        [JsonProperty("toolbar")]
        public Collection<string> ToolBar { get; }
        [JsonProperty("stylesheets")]
        public Collection<object> StyleSheets { get; }
        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }
        [JsonProperty("maxImageSize")]
        public int MaxImageSize { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }

        public GridRtePreValue()
        {
            ToolBar = new Collection<string>();
            StyleSheets = new Collection<object>();
        }
    }

    public class Dimensions
    {
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }

        public Dimensions(int height)
        {
            Height = height;
        }

        public Dimensions(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}