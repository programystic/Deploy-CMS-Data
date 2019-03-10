using DeployCmsData.UmbracoCms.Enums;
using DeployCmsData.UmbracoCms.Models;
using Newtonsoft.Json.Linq;
using System;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Composing;
using Umbraco.Web.PropertyEditors;
using Validation;


namespace DeployCmsData.UmbracoCms.Builders
{
    public class GridDataTypeBuilder
    {
        public const string PreValueItemsName = "items";
        public const string PreValueRteName = "rte";

        public GridItemsPreValue GridItemsPreValue { get; set; }
        public GridRtePreValue GridRtePreValue { get; set; }

        private IDataTypeService DataTypeService { get; set; }
        private string NameValue { get; set; }
        private Guid KeyValue { get; set; }

        public GridDataTypeBuilder(Guid key)
        {
            Verify.Operation(key != Guid.Empty, "key cannot be an empty guid");

            Setup(Current.Services.DataTypeService);
            KeyValue = key;
        }

        public GridDataTypeBuilder(IDataTypeService dataTypeService, Guid key)
        {
            Verify.Operation(key != Guid.Empty, "key cannot be an empty guid");

            Setup(dataTypeService);
            KeyValue = key;
        }

        public void Setup(IDataTypeService dataTypeService)
        {
            Requires.NotNull(dataTypeService, nameof(dataTypeService));

            DataTypeService = dataTypeService;
            GridItemsPreValue = new GridItemsPreValue();
            GridRtePreValue = new GridRtePreValue();
            GridItemsPreValue.Columns = 12;
            KeyValue = Guid.Empty;

            GridItemsPreValue.Styles.Add(new Style()
            {
                Label = "Set a background image",
                Description = "Set a row background",
                Key = "background-image",
                View = "imagepicker",
                Modifier = "url({0})"
            });

            GridItemsPreValue.Config.Add(new Config()
            {
                Label = "Class",
                Description = "Set a css class",
                Key = "class",
                View = "textstring"
            });

            GridRtePreValue.Dimensions = new Dimensions(500);
            GridRtePreValue.MaxImageSize = 500;
            Mode(GridMode.Classic);
        }

        public GridDataTypeBuilder Name(string gridName)
        {
            Requires.NotNullOrWhiteSpace(gridName, nameof(gridName));

            NameValue = gridName;
            return this;
        }

        public GridDataTypeBuilder Columns(int columns)
        {
            Requires.Range(columns > 0, nameof(columns), "Columns must be greater than 0");
            GridItemsPreValue.Columns = columns;

            return this;
        }

        public GridDataTypeBuilder AddLayout(string layoutName, params int[] gridColumns)
        {
            Requires.NotNullOrWhiteSpace(layoutName, nameof(layoutName));
            Requires.NotNull(gridColumns, nameof(gridColumns));

            var template = new Models.Template
            {
                Name = layoutName
            };

            foreach (var column in gridColumns)
            {
                template.Sections.Add(new Models.Section(column));
            }

            GridItemsPreValue.Layouts.Add(template);

            return this;
        }

        public GridDataTypeBuilder AddRow(string rowName, params int[] areas)
        {
            Requires.NotNull(rowName, nameof(rowName));
            Requires.NotNull(areas, nameof(areas));

            var layout = new GridLayout
            {
                Label = rowName,
                Name = rowName
            };

            foreach (var area in areas)
            {
                var newArea = new Area(area)
                {
                    AllowAll = true
                };

                layout.Areas.Add(newArea);
            }

            GridItemsPreValue.Rows.Add(layout);

            return this;
        }

        public GridDataTypeBuilder AddStandardLayouts()
        {
            AddLayout("1 column layout", 12);
            AddLayout("2 column layout", 4, 8);

            return this;
        }

        public GridDataTypeBuilder AddStandardRows()
        {
            AddRow("Headline", 12);
            AddRow("Article", 4, 8);

            return this;
        }

        public GridDataTypeBuilder AddStandardToolBar()
        {
            GridRtePreValue.ToolBar.Add("code");
            GridRtePreValue.ToolBar.Add("styleselect");
            GridRtePreValue.ToolBar.Add("bold");
            GridRtePreValue.ToolBar.Add("italic");
            GridRtePreValue.ToolBar.Add("alignleft");
            GridRtePreValue.ToolBar.Add("aligncenter");
            GridRtePreValue.ToolBar.Add("alignright");
            GridRtePreValue.ToolBar.Add("bullist");
            GridRtePreValue.ToolBar.Add("numlist");
            GridRtePreValue.ToolBar.Add("outdent");
            GridRtePreValue.ToolBar.Add("indent");
            GridRtePreValue.ToolBar.Add("link");
            GridRtePreValue.ToolBar.Add("umbmediapicker");
            GridRtePreValue.ToolBar.Add("umbmacro");
            GridRtePreValue.ToolBar.Add("umbembeddialog");
            return this;
        }

        public GridDataTypeBuilder AddToolBarOption(string option)
        {
            GridRtePreValue.ToolBar.Add(option);
            return this;
        }

        public GridDataTypeBuilder DeleteGrid(string gridName)
        {
            Requires.NotNullOrWhiteSpace(gridName, nameof(gridName));

            DataTypeBuilder.DeleteDataTypeByName(gridName, DataTypeService);
            return this;
        }

        public void DeleteGrid(Guid gridId)
        {
            DataTypeBuilder.DeleteDataTypeById(gridId, DataTypeService);
        }

        public GridDataTypeBuilder Mode(GridMode mode)
        {
            switch (mode)
            {
                case GridMode.Classic:
                    GridRtePreValue.Mode = "classic";
                    break;

                case GridMode.DistractionFree:
                    GridRtePreValue.Mode = "distraction-free";
                    break;

                default:
                    break;
            }

            return this;
        }

        public IDataType Build()
        {
            var gridDataType = DataTypeService.GetDataType(KeyValue);

            if (gridDataType == null)
            {
                var editor = new GridPropertyEditor(Current.Logger);

                gridDataType = new DataType(editor)
                {
                    Name = NameValue
                };

                Verify.Operation(KeyValue != Guid.Empty, "key cannot be an empty guid");
                gridDataType.Key = KeyValue;
            }

            var configuration = new GridConfiguration
            {
                Items = JObject.FromObject(GridItemsPreValue),
                Rte = JObject.FromObject(GridRtePreValue)
            };

            gridDataType.Configuration = configuration;

            DataTypeService.Save(gridDataType);

            return gridDataType;
        }
    }
}