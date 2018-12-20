using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using System.Linq;

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
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataType.Textstring)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

            builder.AddField("name")
                .Name("The Name")
                .DataType(DataType.Textstring)
                .Tab("The First 1");

            builder.AddField("theNumber")
                .Name("The Number")
                .DataType(DataType.Numeric)
                .IsMandatory();

            builder.ReBuild();
        }

        [Test]
        public static void AddNewProperty_DefaultDatType_TextString()
        {
            const string fieldAlias = "myNewProperty";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataType.Textstring)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .AddField(fieldAlias);

            builder.ReBuild();

            var field = builder.AddFieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
            Assert.AreEqual(DataType.Textstring, field.DataTypeValue);
        }

        [Test]
        public static void AddNewProperty_DefaultName()
        {
            const string fieldAlias = "myNewProperty";
            const string fieldName = "My New Property";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataType.Textstring)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder.AddField(fieldAlias);
            builder.ReBuild();

            var field = builder.AddFieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
            Assert.AreEqual(fieldName, field.NameValue);
        }
    }
}
