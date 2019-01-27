using DeployCmsData.Core.Extensions;
using DeployCmsData.UmbracoCms.Models;
using DeployCmsData.UmbracoCms.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class MultiNodeTreePickerBuilder
    {
        public const string PreValueFilter = "filter";
        public const string PreValueMinNumber = "minNumber";
        public const string PreValueMaxNumber = "maxNumber";
        public const string PreValueShowOpenButton = "showOpenButton";
        public const string PreValueStartNode = "startNode";
        private IDataTypeService _dataTypeService;
        private IContentService _contentService;
        private IMediaService _mediaService;
        private string NameValue { get; set; }
        private Guid KeyValue { get; set; }
        private string[] _filter;
        private int _minimumItems;
        private int _maximumItems;
        private bool _showOpenButton;
        private MultiNodeTreePickerStartNodePreValue _startNodepreValue;
        private readonly Dictionary<string, PreValue> preValues;

        public Dictionary<string, PreValue> GetPreValues()
        {
            return preValues;
        }

        public MultiNodeTreePickerBuilder(Guid key)
        {
            _dataTypeService = UmbracoContext.Current.Application.Services.DataTypeService;
            _contentService = UmbracoContext.Current.Application.Services.ContentService;
            _mediaService = UmbracoContext.Current.Application.Services.MediaService;
            KeyValue = key;
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue() { StartNodeType = Enums.StartNodeType.Content };
            preValues = new Dictionary<string, PreValue>();
        }

        public MultiNodeTreePickerBuilder(
            IDataTypeService dataTypeService,
            IContentService contentService,
            IMediaService mediaService,
            Guid key)
        {
            _dataTypeService = dataTypeService;
            _contentService = contentService;
            _mediaService = mediaService;
            KeyValue = key;
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue() { StartNodeType = Enums.StartNodeType.Content };
            preValues = new Dictionary<string, PreValue>();
        }

        public MultiNodeTreePickerBuilder Name(string multiNodeTreePickerName)
        {
            NameValue = multiNodeTreePickerName;
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

        public MultiNodeTreePickerBuilder StartNodeContent(string contentId)
        {
            if (!int.TryParse(contentId, out int id))
            {
                throw new ArgumentException($"This isn't a valid a valid integer - {contentId}", nameof(contentId));
            }

            return StartNodeContent(id);
        }

        public MultiNodeTreePickerBuilder StartNodeContent(int contentId)
        {
            return StartNodeContent(_contentService.GetById(contentId).Key);
        }

        public MultiNodeTreePickerBuilder StartNodeContent(Guid contentId)
        {
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue()
            {
                StartNodeType = Enums.StartNodeType.Content,
                Query = StringFormat.ToInvariant($"umb://document/{contentId}")
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeXpath(string xpath)
        {
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue()
            {
                StartNodeType = Enums.StartNodeType.Content,
                Query = xpath
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeMedia(string mediaId)
        {
            if (!int.TryParse(mediaId, out int id))
            {
                throw new ArgumentException($"This isn't a valid a valid integer - {mediaId}", nameof(mediaId));
            }

            return StartNodeMedia(id);
        }

        public MultiNodeTreePickerBuilder StartNodeMedia(int mediaId)
        {
            return StartNodeMedia(_mediaService.GetById(mediaId).Key);
        }

        public MultiNodeTreePickerBuilder StartNodeMedia(Guid mediaId)
        {
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue()
            {
                StartNodeType = Enums.StartNodeType.Media,
                Query = StringFormat.ToInvariant($"umb://media/{mediaId}")
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeMember()
        {
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue()
            {
                StartNodeType = Enums.StartNodeType.Member,
                Id = "-1"
            };
            return this;
        }

        public IDataTypeDefinition Build()
        {
            var dataType = _dataTypeService.GetDataTypeDefinitionById(KeyValue);
            if (dataType == null)
            {
                dataType = new DataTypeDefinition(-1, Umbraco.Core.Constants.PropertyEditors.MultiNodeTreePicker2Alias);
            }

            dataType.Name = NameValue;

            if (KeyValue != Guid.Empty)
            {
                dataType.Key = KeyValue;
            }

            GetPreValues().Add(PreValueStartNode, new PreValue(JsonHelper.SerializePreValueObject(_startNodepreValue)));
            GetPreValues().Add(PreValueFilter, new PreValue(string.Join(",", _filter)));
            GetPreValues().Add(PreValueMinNumber, new PreValue(_minimumItems.ToString(CultureInfo.InvariantCulture)));
            GetPreValues().Add(PreValueMaxNumber, new PreValue(_maximumItems.ToString(CultureInfo.InvariantCulture)));
            GetPreValues().Add(PreValueShowOpenButton, new PreValue(_showOpenButton ? "1" : "0"));

            _dataTypeService.SaveDataTypeAndPreValues(dataType, GetPreValues());

            return dataType;
        }
    }
}