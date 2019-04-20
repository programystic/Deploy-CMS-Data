using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DeployCmsData.Umbraco7.Models
{

    public class GridItemsPreValue
    {
        public Collection<Style> Styles { get; }
        public Collection<Config> Config { get; }
        public int Columns { get; set; }

        [JsonProperty("Templates")]
        public Collection<Template> Layouts { get; }

        [JsonProperty("Layouts")]
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
        public string Label { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string View { get; set; }
        public string Modifier { get; set; }
    }

    public class Config
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string View { get; set; }
    }

    public class Section
    {
        public int Grid { get; set; }

        public Section(int gridValue)
        {
            Grid = gridValue;
        }
    }

    public class Template
    {
        public string Name { get; set; }
        public Collection<Section> Sections { get; }

        public Template()
        {
            Sections = new Collection<Section>();
        }
    }
}