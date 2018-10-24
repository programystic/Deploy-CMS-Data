using DeployCmsData.Test.Builders;
using DeployCmsData.UmbracoCms.Builders;
using NUnit.Framework;
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
        public void GridViewTemplates()
        {
            var builder = new GridDataTypeTestBuilder()
                .Build();

            var gridDataType = builder
                .Name("My New Grid View")
                .AddStandardToolbar()
                .AddTemplate("1 column", 12)
                .AddTemplate("2 column", 4, 8)
                .AddTemplate("3 column", 4, 4, 4)
                .AddTemplate("4 column", 3, 3, 3)
                .Build();

            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
            Assert.AreEqual("My New Grid View", gridDataType.Name);
            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
            Assert.AreEqual(15, builder.GridRtePreValue.Toolbar.Count);
        }

    }
}
