using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture()]
    public static class CreateNewProperties
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";
        private const int ParentId = 555;

        [Test]
        public static void CreateNewPropertyWithTab()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsNewContentType(ParentId)
                .ReturnsExistingContentType(ParentAlias, ParentId)
                .ReturnsDataType(DataType.TextString)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

            builder.AddField("name")
                .Name("The Name")
                .DataType(DataType.TextString)
                .Tab("The First 1");

            builder.AddField("theNumber")
                .Name("The Number")
                .DataType(DataType.Numeric)
                .IsMandatory()
                .Tab("The Second 1");

            builder.BuildWithParent(ParentAlias);

            setup.ContentType.Verify(x => x.AddPropertyType(
                    It.IsAny<PropertyType>(), It.IsAny<string>()),
                Times.Exactly(2));
        }

        [Test]
        public static void CreateNewPropertyWithNoTab()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsNewContentType(ParentId)
                .ReturnsExistingContentType(ParentAlias, ParentId)
                .ReturnsDataType(DataType.TextString)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

            builder.AddField("name")
                .Name("The Name")
                .DataType(DataType.TextString);

            builder.AddField("theNumber")
                .Name("The Number")
                .DataType(DataType.Numeric)
                .IsMandatory();

            builder.BuildWithParent(ParentAlias);

            setup.ContentType.Verify(x => x.AddPropertyType(
                    It.IsAny<PropertyType>()),
                Times.Exactly(2));
        }

        [Test]
        public static void CreateNewPropertyWithDefaults()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsNewContentType(ParentId)
                .ReturnsExistingContentType(ParentAlias, ParentId)
                .ReturnsDataType(DataType.TextString)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

            builder.AddField("name1");
            builder.AddField("name2");
            builder.AddField("name3");

            builder.BuildWithParent(ParentAlias);

            setup.ContentType.Verify(x => x.AddPropertyType(
                    It.IsAny<PropertyType>()),
                Times.Exactly(3));
        }
    }
}