using System;
using DeployCmsData.Constants;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.Services
{
    public class DataTypeBuilder
    {
        public DataTypeBuilder(ApplicationContext applicationContext)
        {
            //Dictionary<string, PreValue> preValues = new Dictionary<string, PreValue>();

            //preValues.Add("onlyImages", new PreValue("1"));
            //preValues.Add("disableFolderSelect", new PreValue("1"));

            //DataTypeDefinition dataTypeDefinition = new DataTypeDefinition(containerId, "Umbraco.MediaPicker2");

            //dataTypeDefinition.Name = "Single Image Picker";

            //_dataTypeService.SaveDataTypeAndPreValues(dataTypeDefinition, preValues);

            var service = applicationContext.Services.DataTypeService;

            var x1 = new DataTypeDefinition(-1, "testDataType");
            x1.Name = "Test 101";
            x1.PropertyEditorAlias = "Umbraco.TinyMCEv3";
            service.Save(x1);
        }
    }
}
