using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture()]
    class UpdateDocumentType
    {
        private const string Alias = "myAlias";
        private const string Name = "myName";
        private const string Description = "myDescription";
        private const string Icon = "myIcon";
        private const string ParentAlias = "myParentAlias";
        private const int Id = 101;
        private const int ParentId = 555;

        [Test]
        public static void AddNewProperty()
        {
            var setup = new DocumentTypeTestBuilder();
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(CmsDataType.TextString)
                .ReturnsDataType(CmsDataType.Numeric)
                .Build();

            builder
                .Alias(Alias)
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

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

            builder.Build();
        }
    }
}
