using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using System.Linq;
using Umbraco.Core.Models;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture()]
    public static class UpdateDocumentType
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
                .IsMandatory();

            builder.Update();
        }

        [Test]
        public static void AddNewProperty_DefaultDataType_TextString()
        {
            const string fieldAlias = "myNewProperty";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataType.TextString)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder
                .AddField(fieldAlias);

            builder.Update();

            var field = builder.AddFieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
            Assert.AreEqual(DataType.TextString, field.DataTypeValue);
        }

        [Test]
        public static void AddNewProperty_DefaultName()
        {
            const string fieldAlias = "myNewProperty";
            const string fieldName = "My New Property";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataType.TextString)
                .ReturnsDataType(DataType.Numeric)
                .Build();

            builder.AddField(fieldAlias);
            builder.Update();

            var field = builder.AddFieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
            Assert.AreEqual(fieldName, field.NameValue);
        }

        [Test]
        public static void NoUpdates()
        {
            const string icon = "icon";
            const string name = "name";
            const string description = "description";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .Build();

            var docType = setup.ContentTypeService.Object.GetContentType(Alias);            
            docType.Icon = icon;
            docType.Name = name;
            docType.Description = description;

            var updatedDocType = builder.Update();

            Assert.AreEqual(Alias, updatedDocType.Alias);
            Assert.AreEqual(icon, updatedDocType.Icon);
            Assert.AreEqual(name, updatedDocType.Name);
            Assert.AreEqual(description, updatedDocType.Description);
        }

        [Test]
        public static void WithUpdates()
        {
            const string icon = "icon";
            const string name = "name";
            const string description = "description";

            const string newIcon = "new-icon";
            const string newName = "new-name";
            const string newDescription = "new-description";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsExistingContentType(Alias, Id)
                .Build();

            var docType = setup.ContentTypeService.Object.GetContentType(Alias);
            docType.Icon = icon;
            docType.Name = name;
            docType.Description = description;

            var updatedDocType = builder
                .Icon(newIcon)
                .Name(newName)
                .Description(newDescription)
                .Update();

            Assert.AreEqual(Alias, updatedDocType.Alias);
            Assert.AreEqual(newIcon, updatedDocType.Icon);
            Assert.AreEqual(newName, updatedDocType.Name);
            Assert.AreEqual(newDescription, updatedDocType.Description);
        }

        [Test]
        public static void SetDefaultTemplate()
        {
            const string icon = "icon";
            const string name = "name";
            const string description = "description";

            var template = new Mock<ITemplate>();

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsDefaultContentType(Alias, Id)
                .Build();

            var docType = setup.ContentType.Object;
            docType.Icon = icon;
            docType.Name = name;
            docType.Description = description;

            var updatedDocType = builder
                .DefaultTemplate(template.Object)
                .Update();

            Assert.AreEqual(Alias, updatedDocType.Alias);
            Assert.AreEqual(icon, updatedDocType.Icon);
            Assert.AreEqual(name, updatedDocType.Name);
            Assert.AreEqual(description, updatedDocType.Description);

            setup.ContentType.Verify(x => x.SetDefaultTemplate(template.Object), Times.Once);
        }

        [Test]
        public static void AddAllowedChildNodeType()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsDefaultContentType(Alias, Id)                
                .HasAllowedContentType("type1", 10)
                .HasAllowedContentType("type3", 30)
                .ReturnsExistingContentType("type2", 20)
                .ReturnsExistingContentType("type4", 40)
                .Build();

            var updatedDocType = builder
                .AddAllowedChildNodeType("type2")
                .AddAllowedChildNodeType("type4")
                .Update();

            Assert.AreEqual(4, builder.AllowedChildNodeTypesCount());
            Assert.AreEqual(4, updatedDocType.AllowedContentTypes.Count());
        }

        [Test]
        public static void AddAllowedChildNodeTypeWhenNoneSetAlready()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsDefaultContentType(Alias, Id)
                .ReturnsExistingContentType("type1", 10)
                .ReturnsExistingContentType("type2", 20)
                .Build();

            var updatedDocType = builder
                .AddAllowedChildNodeType("type1")
                .AddAllowedChildNodeType("type2")
                .Update();

            Assert.AreEqual(2, builder.AllowedChildNodeTypesCount());
            Assert.AreEqual(2, updatedDocType.AllowedContentTypes.Count());
        }

        [Test]
        public static void AddAllowedChildNodeTypeDuplicates()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .ReturnsDefaultContentType(Alias, Id)
                .HasAllowedContentType("type1", 10)
                .HasAllowedContentType("type2", 20)
                .Build();

            var updatedDocType = builder
                .AddAllowedChildNodeType("type1")
                .AddAllowedChildNodeType("type2")
                .Update();

            Assert.AreEqual(2, builder.AllowedChildNodeTypesCount());
            Assert.AreEqual(2, updatedDocType.AllowedContentTypes.Count());
        }
    }
}