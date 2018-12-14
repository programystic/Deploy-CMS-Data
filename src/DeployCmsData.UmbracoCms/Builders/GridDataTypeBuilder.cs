using System;
using DeployCmsData.UmbracoCms.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;


namespace DeployCmsData.UmbracoCms.Builders
{
    public class GridDataTypeBuilder
    {
        public const string PreValueItemsName = "items";
        public const string PreValueRteName = "rte";

        public GridItemsPreValue GridItemsPreValue { get; set; }
        public GridRtePreValue GridRtePreValue { get; set; }

        IDataTypeService DataTypeService { get; set; }
        string NameValue { get; set; }
        Guid KeyValue { get; set; }
        DataTypeBuilder _dataTypeBuilder;

        public GridDataTypeBuilder(Guid key)
        {
            Setup(UmbracoContext.Current.Application.Services.DataTypeService);
            KeyValue = key;
        }

        public GridDataTypeBuilder(IDataTypeService dataTypeService, Guid key)
        {
            Setup(dataTypeService);
            KeyValue = key;
        }

        public void Setup(IDataTypeService dataTypeService)
        {
            _dataTypeBuilder = new DataTypeBuilder();
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
        }

        public GridDataTypeBuilder Name(string name)
        {
            NameValue = name;
            return this;
        }

        public GridDataTypeBuilder AddLayout(string layoutName, params int[] gridColumns)
        {
            var template = new Models.Template
            {
                Name = layoutName
            };

            foreach (var column in gridColumns)
                template.Sections.Add(new Models.Section(column));

            GridItemsPreValue.Layouts.Add(template);

            return this;
        }

        public GridDataTypeBuilder AddRow(string rowName, params int[] areas)
        {
            var layout = new Layout
            {
                Label = rowName,
                Name = rowName
            };

            foreach (var area in areas)
                layout.Areas.Add(new Area(area));

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

        public GridDataTypeBuilder AddStandardToolbar()
        {
            GridRtePreValue.Toolbar.Add("code");
            GridRtePreValue.Toolbar.Add("styleselect");
            GridRtePreValue.Toolbar.Add("bold");
            GridRtePreValue.Toolbar.Add("italic");
            GridRtePreValue.Toolbar.Add("alignleft");
            GridRtePreValue.Toolbar.Add("aligncenter");
            GridRtePreValue.Toolbar.Add("alignright");
            GridRtePreValue.Toolbar.Add("bullist");
            GridRtePreValue.Toolbar.Add("numlist");
            GridRtePreValue.Toolbar.Add("outdent");
            GridRtePreValue.Toolbar.Add("indent");
            GridRtePreValue.Toolbar.Add("link");
            GridRtePreValue.Toolbar.Add("umbmediapicker");
            GridRtePreValue.Toolbar.Add("umbmacro");
            GridRtePreValue.Toolbar.Add("umbembeddialog");
            return this;
        }

        public GridDataTypeBuilder AddToolbarOption(string option)
        {
            GridRtePreValue.Toolbar.Add(option);
            return this;
        }

        public GridDataTypeBuilder DeleteGrid(string gridName)
        {
            _dataTypeBuilder.DeleteDataTypeByName(gridName, DataTypeService);
            return this;
        }

        public void DeleteGrid(Guid id)
        {
            _dataTypeBuilder.DeleteDataTypeById(id, DataTypeService);
        }

        public void BuildInFolder(string folderName)
        {
            // returns IDataTypeDefinition
        }

        public IDataTypeDefinition Build()
        {
            var newGridDataType = new DataTypeDefinition(-1, ProperyEditors.GridAlias)
            {
                Name = NameValue
            };
            if (KeyValue != Guid.Empty) newGridDataType.Key = KeyValue;

            var itemsJson = SerializeObject(GridItemsPreValue);
            var rteJson = SerializeObject(GridRtePreValue);

            var preValues = new System.Collections.Generic.Dictionary<string, PreValue>
            {
                { PreValueItemsName, new PreValue(itemsJson) },
                { PreValueRteName, new PreValue(rteJson) }
            };

            DataTypeService.SaveDataTypeAndPreValues(newGridDataType, preValues);

            return newGridDataType;
        }

        private string SerializeObject(object value)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(value, serializerSettings);
        }
    }
}