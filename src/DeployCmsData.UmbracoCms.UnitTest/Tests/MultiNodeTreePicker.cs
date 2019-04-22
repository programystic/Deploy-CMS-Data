using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using NUnit.Framework;
using System;
using System.Globalization;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    public static class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            const int maxNumber = 5;
            const int minNumber = 1;

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .Build();

            builder
                .Name("My New Tree Picker")
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            Assert.AreEqual(5, builder.PreValueCount);
            Assert.AreEqual(minNumber.ToString(CultureInfo.InvariantCulture), builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.AreEqual(maxNumber.ToString(CultureInfo.InvariantCulture), builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("1", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }


        [Test]
        public static void PickerWithNoValues()
        {
            const int contentId = 1234;

            var contentGuid = new Guid("{50CC58EB-19A7-4165-B74F-BD9FA0A4F6BD}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, contentGuid)
                .Build();

            builder
                .Name("My New Tree Picker")
                .Build();

            Assert.AreEqual(5, builder.PreValueCount);
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeContentWithGuidId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeContent(key)
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"content\",\"query\":null,\"id\":\"{contentId.ToString()}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeContentWithIntId()
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

            Assert.AreEqual($"{{\"type\":\"content\",\"query\":null,\"id\":\"{contentId.ToString()}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeContentWithStringId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeContent(contentId.ToString())
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"content\",\"query\":null,\"id\":\"{contentId.ToString()}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeContentWithFilter()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");
            var filter = "$parent/newsArticle";

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeXpath(filter)
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"content\",\"query\":\"{filter}\",\"id\":null}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeMediaIntId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeMedia(contentId)
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"media\",\"query\":null,\"id\":\"{contentId}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeMediaGuidId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeMedia(key)
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"media\",\"query\":null,\"id\":\"{contentId}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }

        [Test]
        public static void SetStartNodeMediaStringId()
        {
            const int contentId = 1234;
            Guid key = new Guid("{FD16566D-C9A8-4053-88DF-14EBB3938171}");

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .ContentServiceReturnsContent(contentId, key)
                .Build();

            var dataType = builder
                .Name("My New Tree Picker")
                .StartNodeMedia(contentId.ToString())
                .Build();

            var preValueStartNode = builder.PreValue(MultiNodeTreePickerBuilder.PreValueStartNode);

            Assert.AreEqual($"{{\"type\":\"media\",\"query\":null,\"id\":\"{contentId}\"}}",
                preValueStartNode);

            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueFilter));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMinNumber));
            Assert.IsNull(builder.PreValue(MultiNodeTreePickerBuilder.PreValueMaxNumber));
            Assert.AreEqual("0", builder.PreValue(MultiNodeTreePickerBuilder.PreValueShowOpenButton));
        }
    }
}