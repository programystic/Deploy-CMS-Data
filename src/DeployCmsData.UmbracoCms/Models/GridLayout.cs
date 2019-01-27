using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridLayout
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public Collection<Area> Areas { get; }

        public GridLayout()
        {
            Areas = new Collection<Area>();
        }
    } 
}