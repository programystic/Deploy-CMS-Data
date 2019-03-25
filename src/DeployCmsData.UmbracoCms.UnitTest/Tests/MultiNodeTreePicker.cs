using DeployCmsData.Core.Extensions;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Models;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    public static class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            const int contentId = 1234;
            const int maxNumber = 5;
            const int minNumber = 1;

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");
            var expectedJson = StringFormat.ToInvariant($"{{\"type\":\"content\",\"query\":null,\"id\":\"umb://document/{contentGuid}\"}}");

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

            builder
                .Name("My New Tree Picker")
                .StartNodeContent(contentId)
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            var preValueJson = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.AreEqual(5, builder.PreValueCount);
            Assert.IsTrue(preValue.Id.EndsWith(contentGuid.ToString(), StringComparison.Ordinal));
            Assert.AreEqual(Enums.StartNodeType.Content, preValue.StartNodeType);
            Assert.AreEqual(minNumber.ToString(CultureInfo.InvariantCulture), builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.AreEqual(maxNumber.ToString(CultureInfo.InvariantCulture), builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("1", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void Xpath()
        {
            const int contentId = 1234;
            const int maxNumber = 5;
            const int minNumber = 1;
            const string xPath = "$parent/newsArticle";

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");
            var expectedJson = $"{{\"type\":\"content\",\"query\":\"{xPath}\",\"id\":null}}";

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

            builder
                .Name("My New Tree Picker")
                .StartNodeXpath(xPath)
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            var preValueJson = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.IsTrue(preValue.Query.EndsWith(xPath, StringComparison.Ordinal));
        }

        [Test]
        public static void PickerWithNoValues()
        {
            const int contentId = 1234;

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

            builder
                .Name("My New Tree Picker")
                .Build();
        }

        [Test]
        public static void SetStartNodeContentWithId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeContent(contentId)
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual("{\"type\":\"content\",\"query\":null,\"id\":\"umb://document/fd16566d-c9a8-4053-88df-14ebb3938171\"}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }
    }
}