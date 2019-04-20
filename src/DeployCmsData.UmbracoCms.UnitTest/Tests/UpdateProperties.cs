using DeployCmsData.Umbraco7.Constants;
using DeployCmsData.Umbraco7.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace DeployCmsData.Umbraco7.UnitTest.Tests
{
    internal class UpdateProperties
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";
        private const int Id = 101;
        private const int ParentId = 555;

        [Test]
        public static void DeleteFields()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AddField("field1")
                .AddField("field2")
                .AddField("field3")
                .AddField("field4")
                .Build();

            var docType = builder
                .RemoveField("field2")
                .RemoveField("field3")
                .Update();

            setup.ContentType.Verify(x => x.RemovePropertyType("field1"), Times.Never);
            setup.ContentType.Verify(x => x.RemovePropertyType("field2"), Times.Once);
            setup.ContentType.Verify(x => x.RemovePropertyType("field3"), Times.Once);
            setup.ContentType.Verify(x => x.RemovePropertyType("field4"), Times.Never);
        }

        [Test]
        public static void UpdateFields()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AddField("field1")
                .AddField("field2")
                .AddField("field3")
                .AddField("field4")
                .ReturnsDataType(DataType.TextArea, 1)
                .Build();

            builder.AddField("field1")
                .DataType(DataType.TextArea);

            builder.AddField("field4")
                .DataType(DataType.TextArea);

            var docType = builder.Update();


            setup.UmbracoFactory.Verify(x => x.GetPropertyType(It.IsAny<IContentType>(), "field1"), Times.Once);
            setup.UmbracoFactory.Verify(x => x.GetPropertyType(It.IsAny<IContentType>(), "field2"), Times.Never);
            setup.UmbracoFactory.Verify(x => x.GetPropertyType(It.IsAny<IContentType>(), "field3"), Times.Never);
            setup.UmbracoFactory.Verify(x => x.GetPropertyType(It.IsAny<IContentType>(), "field4"), Times.Once);
        }

        [Test]
        public static void ChangeTab()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AddField("field1")
                .AddField("field2")
                .AddField("field3")
                .AddField("field4")
                .ReturnsDataType(DataType.TextArea, 1)
                .Build();

            builder.AddField("field1")
                .Tab("New");

            builder.AddField("field4")
                .Tab("New");

            var docType = builder.Update();

            setup.ContentType.Verify(x => x.MovePropertyType("field1", "New"), Times.Once);
            setup.ContentType.Verify(x => x.MovePropertyType("field2", It.IsAny<string>()), Times.Never);
            setup.ContentType.Verify(x => x.MovePropertyType("field3", It.IsAny<string>()), Times.Never);
            setup.ContentType.Verify(x => x.MovePropertyType("field4", "New"), Times.Once);
        }

        [Test]
        public static void ChangeDataType()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AddField("field1")
                .AddField("field2")
                .AddField("field3")
                .AddField("field4")
                .ReturnsDataType(DataType.TextArea, 1)
                .ReturnsDataType(DataType.CheckBox, 2)
                .ReturnsDataType(DataType.DropDown, 3)
                .Build();

            builder.AddField("field1")
                .DataType(DataType.CheckBox);

            builder.AddField("field4")
                .DataType(DataType.DropDown);

            var docType = builder.Update();

            Assert.AreEqual(2, setup.UmbracoFactory.Object.GetPropertyType(docType, "field1").DataTypeDefinitionId);
            Assert.AreEqual(0, setup.UmbracoFactory.Object.GetPropertyType(docType, "field2").DataTypeDefinitionId);
            Assert.AreEqual(0, setup.UmbracoFactory.Object.GetPropertyType(docType, "field3").DataTypeDefinitionId);
            Assert.AreEqual(3, setup.UmbracoFactory.Object.GetPropertyType(docType, "field4").DataTypeDefinitionId);
        }
    }
}