using DeployCmsData.UmbracoCms.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;


namespace DeployCmsData.UmbracoCms.Builders
{
    public class GridDataTypeBuilder : DataTypeBuilder
    {
        public const string PreValueItemsName = "items";
        public const string PreValueRteName = "rte";

        public GridItemsPreValue GridItemsPreValue { get; set; }
        public GridRtePreValue GridRtePreValue { get; set; }
        IDataTypeService DataTypeService { get; set; }
        string NameValue { get; set; }

        public GridDataTypeBuilder(IDataTypeService dataTypeService)
        {
            DataTypeService = dataTypeService;
            GridItemsPreValue = new GridItemsPreValue();
            GridRtePreValue = new GridRtePreValue();

            GridItemsPreValue.Columns = 12;

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

        public GridDataTypeBuilder AddTemplate(string templateName, params int[] gridColumns)
        {
            var template = new Models.Template();
            template.Name = templateName;

            foreach (var column in gridColumns)
                template.Sections.Add(new Models.Section(column));

            GridItemsPreValue.Templates.Add(template);

            return this;
        }

        public GridDataTypeBuilder AddStandardTemplates()
        {
            GridItemsPreValue.Templates.Add(new Models.Template()
            {
                Name = "1 column layout",
                Sections = {
                    new Models.Section(12)
                }
            });

            GridItemsPreValue.Templates.Add(new Models.Template()
            {
                Name = "2 column layout",
                Sections = {
                    new Models.Section(4),
                    new Models.Section(8)}
            });

            return this;
        }

        public GridDataTypeBuilder AddStandardLayouts()
        {
            GridItemsPreValue.Layouts.Add(new Layout()
            {
                Label = "Headline",
                Name = "Headline",
                Areas =
                {
                    new Area(12, "headline")
                }
            });

            GridItemsPreValue.Layouts.Add(new Layout()
            {
                Label = "Article",
                Name = "Article",
                Areas =
                {
                    new Area(4),
                    new Area(8)
                }
            });

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

        public GridDataTypeBuilder AddRow()
        {
            return this;
        }

        public GridDataTypeBuilder DeleteGrid(string gridName)
        {
            DeleteDataTypeByName(gridName, DataTypeService);
            return this;
        }

        public void BuildInFolder(string folderName)
        {
            // returns IDataTypeDefinition
        }

        public IDataTypeDefinition Build()
        {
            var newGridDataType = new DataTypeDefinition(-1, ProperyEditors.GridAlias);
            newGridDataType.Name = NameValue;

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var itemsJson = JsonConvert.SerializeObject(GridItemsPreValue, serializerSettings);
            var rteJson = JsonConvert.SerializeObject(GridRtePreValue, serializerSettings);

            var preValues = new System.Collections.Generic.Dictionary<string, PreValue>();
            preValues.Add(PreValueItemsName, new PreValue(itemsJson));
            preValues.Add(PreValueRteName, new PreValue(rteJson));

            DataTypeService.SaveDataTypeAndPreValues(newGridDataType, preValues);

            return newGridDataType;
        }
    }
}
