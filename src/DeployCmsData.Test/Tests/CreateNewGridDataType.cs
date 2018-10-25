using DeployCmsData.Test.Builders;
using NUnit.Framework;
using System;
using Umbraco.Core.Models;
using static Umbraco.Core.Constants;

namespace DeployCmsData.Test.Tests
{
    public class CreateNewGridDataType
    {
        [Test]
        public void DefaultGridView()
        {
            var builder = new GridDataTypeTestBuilder()
                .Build();

            var gridDataType = builder
                .Name("My New Grid View")
                .AddStandardToolbar()
                .Build();

            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
            Assert.AreEqual("My New Grid View", gridDataType.Name);
            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
            Assert.AreEqual(15, builder.GridRtePreValue.Toolbar.Count);
        }

        [Test]
        public void GridViewStandardLayouts()
        {
            var builder = new GridDataTypeTestBuilder()
                .Build();

            var gridDataType = builder
                .Name("My New Grid View")
                .AddStandardLayouts()
                .Build();

            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
            Assert.AreEqual("My New Grid View", gridDataType.Name);
            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
            Assert.AreEqual(2, builder.GridItemsPreValue.Layouts.Count);
        }

        [Test]
        public void GridViewLayouts()
        {
            var builder = new GridDataTypeTestBuilder()
                .Build();

            var gridDataType = builder
                .Name("My New Grid View")
                .AddLayout("1 column layout", 12)
                .AddLayout("2 column layout", 4, 8)
                .AddLayout("3 column layout", 4, 4, 4)
                .AddLayout("4 column layout", 3, 3, 3, 3)
                .Build();

            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
            Assert.AreEqual("My New Grid View", gridDataType.Name);
            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
            Assert.AreEqual(4, builder.GridItemsPreValue.Layouts.Count);
            Assert.AreEqual(1, builder.GridItemsPreValue.Layouts[0].Sections.Count);
            Assert.AreEqual(2, builder.GridItemsPreValue.Layouts[1].Sections.Count);
            Assert.AreEqual(3, builder.GridItemsPreValue.Layouts[2].Sections.Count);
            Assert.AreEqual(4, builder.GridItemsPreValue.Layouts[3].Sections.Count);
        }

        [Test]
        public void GridViewKeySet()
        {
            var key = Guid.Parse("{7829562A-1C09-4E62-B0B5-A80E2542DDF7}");

            var builder = new GridDataTypeTestBuilder()
                .Build();

            var gridDataType = builder
                .Name("My New Grid View")
                .Key(key)
                .Build();

            Assert.AreEqual(key, gridDataType.Key);
        }
    }
}
