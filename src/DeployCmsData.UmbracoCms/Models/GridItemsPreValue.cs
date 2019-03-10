using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;

namespace DeployCmsData.UmbracoCms.Models
{

    public class GridItemsPreValue
    {
        [JsonProperty("styles")]
        public Collection<Style> Styles { get; }
        [JsonProperty("config")]
        public Collection<Config> Config { get; }
        [JsonProperty("columns")]
        public int Columns { get; set; }

        [JsonProperty("templates")]
        public Collection<Template> Layouts { get; }

        [JsonProperty("layouts")]
        public Collection<GridLayout> Rows { get; }

        public GridItemsPreValue()
        {
            Styles = new Collection<Style>();
            Config = new Collection<Config>();
            Layouts = new Collection<Template>();
            Rows = new Collection<GridLayout>();
        }
    }

    public class Style
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("view")]
        public string View { get; set; }
        [JsonProperty("modifier")]
        public string Modifier { get; set; }
    }

    public class Config
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("view")]
        public string View { get; set; }
    }

    public class Section
    {
        [JsonProperty("grid")]
        public int Grid { get; set; }

        public Section(int gridValue)
        {
            Grid = gridValue;
        }
    }

    public class Template
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sections")]
        public Collection<Section> Sections { get; }

        public Template()
        {
            Sections = new Collection<Section>();
        }
    }

    public class Area
    {
        [JsonProperty("grid")]
        public int Grid { get; set; }
        [JsonProperty("editors")]
        public Collection<string> Editors { get; }
        [JsonProperty("maxItems")]
        public int MaxItems { get; set; }
        [JsonProperty("allowAll")]
        public bool AllowAll { get; set; }
        [JsonProperty("allowed")]
        public Collection<string> Allowed { get; }

        public Area(int grid)
        {
            Grid = grid;
        }

        public Area(int grid, params string[] editors)
        {
            Grid = grid;
            Editors = new Collection<string>(editors.ToList());
        }
    }
}