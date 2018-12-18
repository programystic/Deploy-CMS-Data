using System;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Models;
using Moq;
using Newtonsoft.Json;
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
            const int contentId = 1234;
            const int maxNumber = 5;
            const int minNumber = 1;            

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");
            var expectedJson = $"{{\"type\":\"content\",\"query\":\"umb://document/{contentGuid}\"}}";

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
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            var preValueJson = builder.PreValues[MultiNodeTreePickerBuilder.PreValueStartNode].Value;
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.AreEqual(5, builder.PreValues.Count);
            Assert.IsTrue(preValue.Query.EndsWith(contentGuid.ToString()));
            Assert.AreEqual(Enums.StartNodeType.Content, preValue.Type);
            Assert.AreEqual(minNumber.ToString(), builder.PreValues[MultiNodeTreePickerBuilder.PreValueMinNumber].Value);
            Assert.AreEqual(maxNumber.ToString(), builder.PreValues[MultiNodeTreePickerBuilder.PreValueMaxNumber].Value);
            Assert.AreEqual("1", builder.PreValues[MultiNodeTreePickerBuilder.PreValueShowOpenButton].Value);
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

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeXPath(xPath)
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            var preValueJson = builder.PreValues[MultiNodeTreePickerBuilder.PreValueStartNode].Value;
            var preValue = JsonConvert.DeserializeObject<MultiNodeTreePickerStartNodePreValue>(preValueJson);

            Assert.AreEqual(expectedJson, preValueJson);
            Assert.IsTrue(preValue.Query.EndsWith(xPath));
        }
    }
}