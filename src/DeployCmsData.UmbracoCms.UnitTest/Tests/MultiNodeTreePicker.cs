using DeployCmsData.Core.Extensions;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Models;
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
    internal class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            const int contentId = 1234;
            const int maxNumber = 5;
            const int minNumber = 1;

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");
            var expectedJson = StringFormat.ToInvariant($"{{\"type\":\"content\",\"query\":\"umb://document/{contentGuid}\"}}");

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

            var preValueJson = builder.GetPreValues()[MultiNodeTreePickerBuilder.PreValueStartNode].Value;
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.AreEqual(5, builder.GetPreValues().Count);
            Assert.IsTrue(preValue.Query.EndsWith(contentGuid.ToString(), StringComparison.Ordinal));
            Assert.AreEqual(Enums.StartNodeType.Content, preValue.StartNodeType);
            Assert.AreEqual(minNumber.ToString(CultureInfo.InvariantCulture), builder.GetPreValues()[MultiNodeTreePickerBuilder.PreValueMinNumber].Value);
            Assert.AreEqual(maxNumber.ToString(CultureInfo.InvariantCulture), builder.GetPreValues()[MultiNodeTreePickerBuilder.PreValueMaxNumber].Value);
            Assert.AreEqual("1", builder.GetPreValues()[MultiNodeTreePickerBuilder.PreValueShowOpenButton].Value);
        }

        [Test]
        public static void XPath()
        {
            const int contentId = 1234;
            const int maxNumber = 5;
            const int minNumber = 1;
            const string xPath = "$parent/newsArticle";

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");
            var expectedJson = $"{{\"type\":\"content\",\"query\":\"{xPath}\"}}";

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

            var preValueJson = builder.GetPreValues()[MultiNodeTreePickerBuilder.PreValueStartNode].Value;
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.IsTrue(preValue.Query.EndsWith(xPath, StringComparison.Ordinal));
        }
    }
}