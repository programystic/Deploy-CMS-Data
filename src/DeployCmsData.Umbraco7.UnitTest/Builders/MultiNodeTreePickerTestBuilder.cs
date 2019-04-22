using DeployCmsData.Umbraco7.Builders;
using Moq;
using System;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.Umbraco7.UnitTest.Builders
{
    internal class MultiNodeTreePickerTestBuilder
    {
        private readonly MultiNodeTreePickerBuilder _builder;
        private Mock<IContentService> _contentService;
        private Mock<IMediaService> _mediaService;

        public MultiNodeTreePickerTestBuilder(Guid key)
        {
            var dataTypeService = new Mock<IDataTypeService>();
            _mediaService = new Mock<IMediaService>();
            _contentService = new Mock<IContentService>();

            _builder = new MultiNodeTreePickerBuilder(
                dataTypeService.Object,
                _contentService.Object,
                _mediaService.Object,
                key);
        }

        public MultiNodeTreePickerTestBuilder ContentServiceReturnsContent(int contentId, Guid key)
        {
            var content = new Mock<IContent>();
            content.SetupGet(x => x.Key).Returns(key);
            content.SetupGet(x => x.Id).Returns(contentId);

            _contentService.Setup(x => x.GetById(contentId)).Returns(content.Object);
            _contentService.Setup(x => x.GetById(key)).Returns(content.Object);

            var media = new Mock<IMedia>();
            media.SetupGet(x => x.Key).Returns(key);
            media.SetupGet(x => x.Id).Returns(contentId);

            _mediaService.Setup(x => x.GetById(contentId)).Returns(media.Object);
            _mediaService.Setup(x => x.GetById(key)).Returns(media.Object);

            return this;
        }

        public MultiNodeTreePickerBuilder Build()
        {
            return _builder;
        }
    }
}
