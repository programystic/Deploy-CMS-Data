using System;
using DeployCmsData.UmbracoCms.Constants;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class MultiNodeTreePickerBuilder
    {
        private IDataTypeService DataTypeService { get; set; }
        private string NameValue { get; set; }
        private Guid KeyValue { get; set; }
        private string[] _filter;
        private int _minimumItems;
        private int _maximumItems;
        private bool _showOpenButton;

        public MultiNodeTreePickerBuilder(Guid key)
        {
            Setup(UmbracoContext.Current.Application.Services.DataTypeService);
            KeyValue = key;
        }

        public MultiNodeTreePickerBuilder(IDataTypeService dataTypeService, Guid key)
        {
            Setup(dataTypeService);
            KeyValue = key;
        }

        public void Setup(IDataTypeService dataTypeService)
        {
            DataTypeService = dataTypeService;
            KeyValue = Guid.Empty;
        }

        public MultiNodeTreePickerBuilder Name(string name)
        {
            NameValue = name;
            return this;
        }

        public MultiNodeTreePickerBuilder NodeType(MultiNodeTreePickerNodeType nodeTye)
        {
            return this;
        }

        public MultiNodeTreePickerBuilder AllowItemsOfType(params string[] types)
        {
            _filter = types;
            return this;
        }

        public MultiNodeTreePickerBuilder MinimumNumberOfItems(int number)
        {
            _minimumItems = number;
            return this;
        }

        public MultiNodeTreePickerBuilder MaximumNumberOfItems(int number)
        {
            _maximumItems = number;
            return this;
        }

        public MultiNodeTreePickerBuilder ShowOpenButton()
        {
            _showOpenButton = true;
            return this;
        }

        public MultiNodeTreePickerBuilder HideOpenButton()
        {
            _showOpenButton = false;
            return this;
        }

        public IDataTypeDefinition Build()
        {
            var preValues = new System.Collections.Generic.Dictionary<string, PreValue>();

            var newGridDataType = new DataTypeDefinition(-1, ProperyEditors.MultiNodeTreePicker2Alias)
            {
                Name = NameValue
            };

            if (KeyValue != Guid.Empty)
            {
                newGridDataType.Key = KeyValue;
            }

            preValues.Add("filter", new PreValue(string.Join(",", _filter)));
            preValues.Add("minNumber", new PreValue(_minimumItems.ToString()));
            preValues.Add("maxNumber", new PreValue(_maximumItems.ToString()));
            preValues.Add("showOpenButton", new PreValue(_showOpenButton ? "1" : "0"));

            // Content
            // startNode / {"type": "content",  "id": "umb://document/c9fb075f50b64460bcf79459eb7893bf"}             

            // Media
            // startNode / {"type": "media",  "id": "umb://media/c30d8961adc64078a051a0b555c96465"}

            // Member
            // startNode / {"type": "member",  "id": -1,  "query": ""}

            DataTypeService.SaveDataTypeAndPreValues(newGridDataType, preValues);

            return newGridDataType;
        }
    }
}