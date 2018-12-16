using System;
using System.Collections.Generic;
using DeployCmsData.UmbracoCms.Models;
using DeployCmsData.UmbracoCms.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;

namespace DeployCmsData.UmbracoCms.Builders
{
    public enum StartNodeType
    {
        Content,
        Media,
        Member
    }

    public class MultiNodeTreePickerBuilder
    {
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

        public const string PreValueFilter = "filter";
        public const string PreValueMinNumber = "minNumber";
        public const string PreValueMaxNumber = "maxNumber";
        public const string PreValueShowOpenButton = "showOpenButton";
        public const string PreValueStartNode = "startNode";

        public MultiNodeTreePickerBuilder(Guid key)
        {
            _dataTypeService = UmbracoContext.Current.Application.Services.DataTypeService;
            _contentService = UmbracoContext.Current.Application.Services.ContentService;
            _mediaService = UmbracoContext.Current.Application.Services.MediaService;
            KeyValue = key;
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue() { Type = nameof(StartNodeType.Content).ToLower() };
        }

        public MultiNodeTreePickerBuilder(
            IDataTypeService dataTypeService,
            IContentService contentService,
            IMediaService mediaService,
            IMemberService memberService,
            Guid key)
        {
            _dataTypeService = dataTypeService;
            _contentService = contentService;
            _mediaService = mediaService;
            KeyValue = key;
        }

        public MultiNodeTreePickerBuilder Name(string name)
        {
            NameValue = name;
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
                Type = nameof(StartNodeType.Content).ToLower(),
                Query = $"umb://document/{contentId}"
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
                Type = nameof(StartNodeType.Media).ToLower(),
                Query = $"umb://media/{mediaId}"
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeMember()
        {
            _startNodepreValue = new MultiNodeTreePickerStartNodePreValue()
            {
                Type = nameof(StartNodeType.Member).ToLower(),
                Id = "-1"
            };
            return this;
        }

        public IDataTypeDefinition Build()
        {
            var preValues = new Dictionary<string, PreValue>();

            var newGridDataType = new DataTypeDefinition(-1, ProperyEditors.MultiNodeTreePicker2Alias)
            {
                Name = NameValue
            };

            if (KeyValue != Guid.Empty)
            {
                newGridDataType.Key = KeyValue;
            }

            preValues.Add(PreValueStartNode, new PreValue(JsonHelper.SerializePreValueObject(_startNodepreValue)));
            preValues.Add(PreValueFilter, new PreValue(string.Join(",", _filter)));
            preValues.Add(PreValueMinNumber, new PreValue(_minimumItems.ToString()));
            preValues.Add(PreValueMaxNumber, new PreValue(_maximumItems.ToString()));
            preValues.Add(PreValueShowOpenButton, new PreValue(_showOpenButton ? "1" : "0"));
            
            _dataTypeService.SaveDataTypeAndPreValues(newGridDataType, preValues);

            return newGridDataType;
        }
    }
}