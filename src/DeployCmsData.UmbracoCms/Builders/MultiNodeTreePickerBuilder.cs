using DeployCmsData.Core.Extensions;
using DeployCmsData.UmbracoCms.Enums;
using DeployCmsData.UmbracoCms.Models;
using System;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.PropertyEditors;
using System.Linq;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class MultiNodeTreePickerBuilder
    {
        public MultiNodePickerConfiguration Configuration { get; private set; }

        private IDataTypeService _dataTypeService;
        private IContentService _contentService;
        private IMediaService _mediaService;
        private string NameValue { get; set; }
        private Guid KeyValue { get; set; }
        private string[] _filter;
        private int _minimumItems;
        private int _maximumItems;
        private bool _showOpenButton;
        private MultiNodeTreePickerStartNodeConfiguration _startNodeConfiguration;

        public MultiNodeTreePickerBuilder(Guid key)
        {
            _dataTypeService = Current.Services.DataTypeService;
            _contentService = Current.Services.ContentService;
            _mediaService = Current.Services.MediaService;
            KeyValue = key;
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration() { StartNodeType = Enums.StartNodeType.Content };
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
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration() { StartNodeType = Enums.StartNodeType.Content };
        }

        public MultiNodeTreePickerBuilder Name(string multiNodeTreePickerName)
        {
            NameValue = multiNodeTreePickerName;
            return this;
        }

        public MultiNodeTreePickerBuilder AllowItemsOfType(params string[] types)
        {
            _filter = types.Select(x => x.Trim()).ToArray();
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
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration()
            {
                StartNodeType = StartNodeType.Content,
                Query = StringFormat.ToInvariant($"umb://document/{contentId}")
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeXpath(string xpath)
        {
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration()
            {
                StartNodeType = StartNodeType.Content,
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
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration()
            {
                StartNodeType = Enums.StartNodeType.Media,
                Query = StringFormat.ToInvariant($"umb://media/{mediaId}")
            };
            return this;
        }

        public MultiNodeTreePickerBuilder StartNodeMember()
        {
            _startNodeConfiguration = new MultiNodeTreePickerStartNodeConfiguration()
            {
                StartNodeType = Enums.StartNodeType.Member,
                Id = "-1"
            };
            return this;
        }

        public IDataType Build()
        {
            var dataType = _dataTypeService.GetDataType(KeyValue);
            if (dataType == null)
            {
                var editor = new MultiNodeTreePickerPropertyEditor(Current.Logger);
                dataType = new DataType(editor);
            }

            dataType.Name = NameValue;

            if (KeyValue != Guid.Empty)
            {
                dataType.Key = KeyValue;
            }

            SetConfigurationAndSave(dataType);

            return dataType;
        }

        private void SetConfigurationAndSave(IDataType dataType)
        {
            Configuration = new MultiNodePickerConfiguration
            {
                Filter = _filter == null ? null : string.Join(",", _filter),
                MinNumber = _minimumItems,
                MaxNumber = _maximumItems,
                ShowOpen = _showOpenButton,
            };

            SetConfigurationTreeSource();

            dataType.Configuration = Configuration;
            _dataTypeService.Save(dataType);
        }

        private void SetConfigurationTreeSource()
        {
            if (!string.IsNullOrWhiteSpace(_startNodeConfiguration.Id))
            {
                Configuration.TreeSource = new MultiNodePickerConfigurationTreeSource
                {
                    StartNodeId = new GuidUdi(new Uri(_startNodeConfiguration.Id)),
                    ObjectType = Enum.GetName(typeof(StartNodeType), _startNodeConfiguration.StartNodeType)
                };
                return;
            }

            if (!string.IsNullOrWhiteSpace(_startNodeConfiguration.Query))
            {
                Configuration.TreeSource = new MultiNodePickerConfigurationTreeSource
                {
                    ObjectType = Enum.GetName(typeof(StartNodeType), _startNodeConfiguration.StartNodeType),
                    StartNodeQuery = _startNodeConfiguration.Query
                };
            }
        }
    }
}