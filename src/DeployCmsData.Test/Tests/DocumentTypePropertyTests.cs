using System;
using DeployCmsData.Constants;
using DeployCmsData.Test.Services;
using NUnit.Framework;

namespace DeployCmsData.Test.Tests
{
    [TestFixture()]
    public static class DocumentTypePropertyTests
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";

        [Test]
        public static void DocumentTypePropertyCreate()
        {            
            var builder = new DocumentTypeBuilderSetup()
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

            var docType = builder.BuildWithParent(ParentAlias);
        }
    }
}
