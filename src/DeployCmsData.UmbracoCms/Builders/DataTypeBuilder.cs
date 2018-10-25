using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class DataTypeBuilder
    {
        public void DeleteDataTypeByName(string dataTypeName, IDataTypeService dataTypeService)
        {
            var dataType = dataTypeService.GetDataTypeDefinitionByName(dataTypeName);
            if (dataType != null) dataTypeService.Delete(dataType);
        }

        public void DeleteDataTypeById(Guid id, IDataTypeService dataTypeService)
        {
            var dataType = dataTypeService.GetDataTypeDefinitionById(id);
            if (dataType != null) dataTypeService.Delete(dataType);
        }

        public const string items = "{" +
  "\"styles\": [" +
   " {" +
      "\"label\": \"Set a background image\"," +
      "\"description\": \"Set a row background\"," +
      "\"key\": \"background-image\"," +
      "\"view\": \"imagepicker\"," +
      "\"modifier\": \"url({0})\"" +
    "}" +
  "]," +
  "\"config\": [" +
   " {" +
   "   \"label\": \"Class\"," +
   "   \"description\": \"Set a css class\"," +
   "   \"key\": \"class\"," +
   "   \"view\": \"textstring\"" +
   " }" +
  "]," +
  "\"columns\": 12," +
  "\"templates\": [" +
  "  {" +
  "    \"name\": \"1 column layout\"," +
  "    \"sections\": [" +
  "      {" +
  "        \"grid\": 12" +
  "      }" +
  "    ]" +
  "  }," +
  "  {" +
  "    \"name\": \"2 column layout\"," +
  "    \"sections\": [" +
  "      {" +
  "        \"grid\": 4" +
  "      }," +
  "      {" +
  "        \"grid\": 8" +
  "      }" +
  "    ]" +
  "  }" +
  "]," +
  "\"layouts\": [" +
  "  {" +
  "    \"label\": \"Headline\"," +
  "    \"name\": \"Headline\"," +
  "    \"areas\": [" +
  "      {" +
  "        \"grid\": 12," +
  "        \"editors\": [" +
  "          \"headline\"" +
  "        ]" +
  "      }" +
  "    ]" +
  "  }," +
  "  {" +
  "    \"label\": \"Article\"," +
  "    \"name\": \"Article\"," +
  "    \"areas\": [" +
  "      {" +
  "        \"grid\": 4" +
  "      }," +
  "      {" +
  "        \"grid\": 8" +
  "      }" +
  "    ]" +
  "  }" +
  "]" +
"}";

        public const string rte =
 "       {" +
"  \"toolbar\": [" +
    "\"code\"," +
    "\"styleselect\"," +
    "\"bold\"," +
    "\"italic\"," +
    "\"alignleft\"," +
    "\"aligncenter\"," +
    "\"alignright\"," +
    "\"bullist\"," +
    "\"numlist\"," +
    "\"outdent\"," +
    "\"indent\"," +
    "\"link\"," +
    "\"umbmediapicker\"," +
    "\"umbmacro\"," +
    "\"umbembeddialog\"" +
  "]," +
  "\"stylesheets\": []," +
  "\"dimensions\": {" +
  "  \"height\": 500" +
  "}," +
  "\"maxImageSize\": 500" +
"}";


    //public DataTypeBuilder(ApplicationContext applicationContext)
    //    {
    //        //Dictionary<string, PreValue> preValues = new Dictionary<string, PreValue>();

    //        //preValues.Add("onlyImages", new PreValue("1"));
    //        //preValues.Add("disableFolderSelect", new PreValue("1"));

    //        //DataTypeDefinition dataTypeDefinition = new DataTypeDefinition(containerId, "Umbraco.MediaPicker2");

    //        //dataTypeDefinition.Name = "Single Image Picker";

    //        //_dataTypeService.SaveDataTypeAndPreValues(dataTypeDefinition, preValues);

    //        var service = applicationContext.Services.DataTypeService;

    //        var dtd1 = service.GetDataTypeDefinitionByName("Test 101");
    //        if (dtd1 != null) service.Delete(dtd1);

    //        var x1 = new DataTypeDefinition(-1, "testDataType");
    //        x1.Name = "Test 101";
    //        x1.PropertyEditorAlias = ProperyEditors.TinyMCEAlias;
    //        service.Save(x1);

    //        var dtd2 = service.GetDataTypeDefinitionByName("Test 202");
    //        if (dtd2 != null) service.Delete(dtd2);

    //        var x2 = new DataTypeDefinition(-1, "testDataType2");
    //        x2.Name = "Test 202";
    //        x2.PropertyEditorAlias = ProperyEditors.GridAlias;

    //        var preValues = new System.Collections.Generic.Dictionary<string, PreValue>();
    //        preValues.Add("items", new PreValue(items));
    //        preValues.Add("rte", new PreValue(rte));

    //        service.SaveDataTypeAndPreValues(x2, preValues);
    //    }
    }
}
