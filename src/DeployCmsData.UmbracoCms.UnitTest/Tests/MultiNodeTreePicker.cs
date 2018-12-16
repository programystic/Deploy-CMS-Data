using System;
using DeployCmsData.UmbracoCms.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    internal class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            var contentId = 1234;
            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");

            var content = new Mock<IContent>();
            content.Setup(x => x.Key).Returns(contentGuid);

            var mediaService = new Mock<IMediaService>();
            var dataTypeService = new Mock<IDataTypeService>();
            var contentService = new Mock<IContentService>();
            contentService.Setup(x => x.GetById(contentId)).Returns(content.Object);

            var builder = new MultiNodeTreePickerBuilder(
                dataTypeService.Object,
                contentService.Object,
                mediaService.Object,
                Guid.NewGuid());

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeContent(contentId)
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(1)
                .MaximumNumberOfItems(5)
                .ShowOpenButton()
                .Build();

            // TODO: Assert builder.PreValues
        }
    }
}