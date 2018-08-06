using System;
using DeployCmsData.Constants;
using DeployCmsData.Test.Services;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace DeployCmsData.Test.Tests
{
    [TestFixture()]
    public static class CreateNewProperties
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";

        [Test]
        public static void CreateNewProperty()
        {
            var setup = new DocumentTypeBuilderSetup();
            var builder = setup
                .ReturnsNewContentType(ValueConstants.RootFolder, ParentAlias)
                .ReturnsDataType(CmsDataType.TextString)
                .ReturnsDataType(CmsDataType.Numeric)
                .Build();

            builder
                .Alias(Alias)
                .Icon(Icon)
                .Name(Name)
                .Description(Description)
                .AddTab("The First 1");

            builder.AddField()
                .Name("The Name")
                .Alias("name")
                .DataType(CmsDataType.TextString)
                .Tab("The First 1");

            builder.AddField()
                .Name("The Number")
                .Alias("theNumber")
                .DataType(CmsDataType.Numeric)
                .IsMandatory();

            builder.BuildWithParent(ParentAlias);

            setup.ContentType.Verify(x => x.AddPropertyType(
                    It.IsAny<PropertyType>(), It.IsAny<string>()),
                Times.Exactly(2));
        }
    }
}
