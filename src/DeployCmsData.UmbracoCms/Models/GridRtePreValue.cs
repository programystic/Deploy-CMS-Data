using System.Collections.Generic;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridRtePreValue
    {
        public List<string> Toolbar { get; set; }
        public List<object> Stylesheets { get; set; }
        public Dimensions Dimensions { get; set; }
        public int MaxImageSize { get; set; }

        public GridRtePreValue()
        {
            Toolbar = new List<string>();
            Stylesheets = new List<object>();
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
