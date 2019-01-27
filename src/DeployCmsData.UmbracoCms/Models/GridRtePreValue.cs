using System.Collections.ObjectModel;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridRtePreValue
    {
        public Collection<string> ToolBar { get; }
        public Collection<object> StyleSheets { get; }
        public Dimensions Dimensions { get; set; }
        public int MaxImageSize { get; set; }

        public GridRtePreValue()
        {
            ToolBar = new Collection<string>();
            StyleSheets = new Collection<object>();
        }
    }

    public class Dimensions
    {
        public int Height { get; set; }
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
