using DeployCmsData.UmbracoCms.UnitTest.Builders;
using NUnit.Framework;
using System;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    public static class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .Build();

            builder
                .Name("My New Tree Picker")
                .Build();

            Assert.IsNull(builder.Configuration.TreeSource);
            Assert.IsFalse(builder.Configuration.ShowOpen);
            Assert.AreEqual(0, builder.Configuration.MinNumber);
            Assert.AreEqual(0, builder.Configuration.MaxNumber);
            Assert.IsNull(builder.Configuration.Filter);
        }

        [Test]
        public static void Filter()
        {
            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .Build();

            builder
                .Name("My New Tree Picker")
                .AllowItemsOfType("type1", "type2", " type3", " type4 ")
                .ShowOpenButton()
                .Build();

            Assert.IsNull(builder.Configuration.TreeSource);
            Assert.IsTrue(builder.Configuration.ShowOpen);
            Assert.AreEqual("type1,type2,type3,type4", builder.Configuration.Filter);
        }

        [Test]
        public static void MinAndMaxItems()
        {
            const int maxNumber = 5;
            const int minNumber = 1;

            var builder = new MultiNodeTreePickerTestBuilder(Guid.NewGuid())
                .Build();

            builder
                .Name("My New Tree Picker")
                .MinimumNumberOfItems(minNumber)
                .MaximumNumberOfItems(maxNumber)
                .ShowOpenButton()
                .Build();

            Assert.IsNull(builder.Configuration.TreeSource);
            Assert.IsTrue(builder.Configuration.ShowOpen);
            Assert.AreEqual(minNumber, builder.Configuration.MinNumber);
            Assert.AreEqual(maxNumber, builder.Configuration.MaxNumber);
            Assert.IsNull(builder.Configuration.Filter);
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
            
            Assert.AreEqual("content", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://document/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("document", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
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

            Assert.AreEqual("content", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://document/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("document", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
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

            Assert.AreEqual("content", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://document/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("document", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
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

            Assert.AreEqual("content", builder.Configuration.TreeSource.ObjectType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeId);
            Assert.AreEqual(filter, builder.Configuration.TreeSource.StartNodeQuery);
        }

        [Test]
        public static void SetStartNodeMediaWithGuidId()
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

            Assert.AreEqual("media", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://media/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("media", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
        }

        [Test]
        public static void SetStartNodeMediaWithIntId()
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

            Assert.AreEqual("media", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://media/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("media", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
        }

        [Test]
        public static void SetStartNodeMediaWithStringId()
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

            Assert.AreEqual("media", builder.Configuration.TreeSource.ObjectType);
            Assert.AreEqual($"umb://media/{key.ToString().ToLower().Replace("-", "")}", builder.Configuration.TreeSource.StartNodeId.ToString());
            Assert.AreEqual("media", builder.Configuration.TreeSource.StartNodeId.EntityType);
            Assert.IsNull(builder.Configuration.TreeSource.StartNodeQuery);
        }

    }
}