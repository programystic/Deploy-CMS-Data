using DeployCmsData.Umbraco7.Constants;
using DeployCmsData.Umbraco7.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using System.Linq;
using Umbraco.Core.Models;

namespace DeployCmsData.Umbraco7.UnitTest.Tests
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .ReturnsDataType(DataTypeAlias.TextString, 1)
                .ReturnsDataType(DataTypeAlias.Numeric, 2)
                .Build();

            builder
                .Icon(Icon)
                .Name(Name)
                .Description(Description);

            builder.AddField("name")
                .Name("The Name")
                .DataTypeAlias(DataTypeAlias.TextString)
                .Tab("The First 1");

            builder.AddField("theNumber")
                .Name("The Number")
                .DataTypeAlias(DataTypeAlias.Numeric)
                .IsMandatory();

            builder.Update();
        }

        [Test]
        public static void AddNewProperty_DefaultDataType_TextString()
        {
            const string fieldAlias = "myNewProperty";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .ReturnsDataType(DataTypeAlias.TextString, 1)
                .ReturnsDataType(DataTypeAlias.Numeric, 2)
                .Build();

            builder
                .AddField(fieldAlias);

            builder.Update();

            var field = builder.FieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
            Assert.AreEqual(DataTypeAlias.TextString, field.DataTypeAliasValue);
        }

        [Test]
        public static void AddNewProperty_DefaultName()
        {
            const string fieldAlias = "myNewProperty";
            const string fieldName = "My New Property";

            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .ReturnsExistingContentType(Alias, Id)
                .ReturnsDataType(DataTypeAlias.TextString, 1)
                .ReturnsDataType(DataTypeAlias.Numeric, 2)
                .Build();

            builder.AddField(fieldAlias);
            builder.Update();

            var field = builder.FieldList.FirstOrDefault(x => x.AliasValue == fieldAlias);
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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
                .SetupExistingDocumentType(Alias, Id, ParentId)
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

        [Test]
        public static void RemoveAllowedChildNodeType()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .HasAllowedContentType("type1", 10)
                .HasAllowedContentType("type2", 20)
                .HasAllowedContentType("type3", 30)
                .Build();

            var updatedDocType = builder
                .RemoveAllowedChildNodeType("type2")
                .Update();

            Assert.AreEqual(2, builder.AllowedChildNodeTypesCount());
            Assert.AreEqual(2, updatedDocType.AllowedContentTypes.Count());
        }

        [Test]
        public static void SetNewTabSortOrder()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .Build();

            var updatedDocType = builder
                .TabSortOrder("tab 1", 10)
                .TabSortOrder("tab 2", 20)
                .TabSortOrder("tab 3", 30)
                .Update();

            Assert.AreEqual(3, updatedDocType.PropertyGroups.Count);
            Assert.AreEqual("tab 1", updatedDocType.PropertyGroups[0].Name);
        }

        [Test]
        public static void UpdateTabSortOrder()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AddExistingTab("tab 1", 1)
                .AddExistingTab("tab 2", 2)
                .AddExistingTab("tab 3", 3)
                .Build();

            var updatedDocType = builder
                .TabSortOrder("tab 1", 10)
                .TabSortOrder("tab 2", 20)
                .TabSortOrder("tab 3", 30)
                .TabSortOrder("tab 4", 40)
                .Update();

            Assert.AreEqual(4, updatedDocType.PropertyGroups.Count);
            Assert.AreEqual("tab 1", updatedDocType.PropertyGroups[0].Name);
            Assert.AreEqual("tab 2", updatedDocType.PropertyGroups[1].Name);
            Assert.AreEqual("tab 3", updatedDocType.PropertyGroups[2].Name);
            Assert.AreEqual("tab 4", updatedDocType.PropertyGroups[3].Name);

            Assert.AreEqual(10, updatedDocType.PropertyGroups[0].SortOrder);
            Assert.AreEqual(20, updatedDocType.PropertyGroups[1].SortOrder);
            Assert.AreEqual(30, updatedDocType.PropertyGroups[2].SortOrder);
            Assert.AreEqual(40, updatedDocType.PropertyGroups[3].SortOrder);
        }

        [Test]
        public static void AllowAtRootRemainsTrue()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AllowAtRoot()
                .Build();

            var docType = builder.Update();

            Assert.IsTrue(docType.AllowedAsRoot);
        }

        [Test]
        public static void AllowAtRootRemainsFalse()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .Build();

            var docType = builder.Update();

            Assert.IsFalse(docType.AllowedAsRoot);
        }

        [Test]
        public static void AllowAtRootChangesFromTrueToFalse()
        {
            var setup = new DocumentTypeTestBuilder(Alias);
            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .AllowAtRoot()
                .Build();

            var docType = builder
                .NoAllowedAsRoot()
                .Update();

            Assert.IsFalse(docType.AllowedAsRoot);
        }

        [Test]
        public static void AllowAtRootChangesFromFalseToTrue()
        {
            var setup = new DocumentTypeTestBuilder(Alias);

            var builder = setup
                .SetupExistingDocumentType(Alias, Id, ParentId)
                .Build();

            var docType = builder
                .AllowedAsRoot()
                .Update();

            Assert.IsTrue(docType.AllowedAsRoot);
        }
    }
}