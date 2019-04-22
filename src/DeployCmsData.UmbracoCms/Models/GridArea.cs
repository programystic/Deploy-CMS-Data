using System.Collections.ObjectModel;
using System.Linq;

namespace DeployCmsData.UmbracoCms.Models
{
    public class GridArea
    {
        public int Grid { get; set; }
        public Collection<string> Editors { get; }
        public int MaxItems { get; set; }
        public bool AllowAll { get; set; }
        public Collection<string> Allowed { get; }

        public GridArea(int grid)
        {
            Grid = grid;
            AllowAll = true;
        }

        public GridArea(int grid, params string[] editors)
        {
            Grid = grid;
            AllowAll = editors == null;
            Editors = editors == null ? null : new Collection<string>(editors.ToList());
        }

        public GridArea(int grid, int maximumItems, params string[] editors)
        {
            Grid = grid;
            AllowAll = editors == null;
            MaxItems = maximumItems;
            Editors = editors == null ? null : new Collection<string>(editors.ToList());
        }
    }
}