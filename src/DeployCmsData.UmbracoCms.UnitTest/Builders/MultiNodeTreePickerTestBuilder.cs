using DeployCmsData.UmbracoCms.Builders;
using Moq;
using System;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Builders
{
    internal class MultiNodeTreePickerTestBuilder
    {
        private readonly MultiNodeTreePickerBuilder _builder;
        private Mock<IContentService> _contentService;

        public MultiNodeTreePickerTestBuilder(Guid key)
        {
            var mediaService = new Mock<IMediaService>();
            var dataTypeService = new Mock<IDataTypeService>();
            _contentService = new Mock<IContentService>();

            _builder = new MultiNodeTreePickerBuilder(
                dataTypeService.Object,
                _contentService.Object,
                mediaService.Object,
                key);
        }

        public MultiNodeTreePickerTestBuilder ContentServiceReturnsContent(int contentId, Guid key)
        {
            var content = new Mock<IContent>();
            content.SetupGet(x => x.Key).Returns(key);
            _contentService.Setup(x => x.GetById(contentId)).Returns(content.Object);

            return this;
        }

        public MultiNodeTreePickerBuilder Build()
        {
            return _builder;
        }
    }
}
