using Newtonsoft.Json;

namespace DeployCmsData.PreValues
{
    public class Grid
    {
        [JsonProperty("styles")]
        public Config[] Styles { get; set; }

        [JsonProperty("config")]
        public Config[] Config { get; set; }

        [JsonProperty("columns")]
        public long Columns { get; set; }

        [JsonProperty("templates")]
        public Template[] Templates { get; set; }

        [JsonProperty("layouts")]
        public Layout[] Layouts { get; set; }
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

        [JsonProperty("modifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Modifier { get; set; }
    }

    public class Layout
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("areas")]
        public Area[] Areas { get; set; }
    }

    public class Area
    {
        [JsonProperty("grid")]
        public long Grid { get; set; }

        [JsonProperty("editors", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Editors { get; set; }
    }

    public class Template
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sections")]
        public Section[] Sections { get; set; }
    }

    public class Section
    {
        [JsonProperty("grid")]
        public long Grid { get; set; }
    }
}
