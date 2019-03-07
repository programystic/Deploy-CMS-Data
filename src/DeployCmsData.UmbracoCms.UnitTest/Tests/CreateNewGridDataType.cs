//using DeployCmsData.UmbracoCms.UnitTest.Builders;
//using NUnit.Framework;
//using System;
//using Umbraco.Core.Models;
//using static Umbraco.Core.Constants;

//namespace DeployCmsData.UmbracoCms.UnitTest.Tests
//{
//    [TestFixture]
//    public static class CreateNewGridDataType
//    {
//        [Test]
//        public static void DefaultGridView()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            var gridDataType = builder
//                .Name("My New Grid View")
//                .AddStandardToolBar()
//                .Build();

//            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
//            Assert.AreEqual("My New Grid View", gridDataType.Name);
//            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
//            Assert.AreEqual(15, builder.GridRtePreValue.ToolBar.Count);
//        }

//        [Test]
//        public static void GridViewStandardLayouts()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            var gridDataType = builder
//                .Name("My New Grid View")
//                .AddStandardLayouts()
//                .Build();

//            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
//            Assert.AreEqual("My New Grid View", gridDataType.Name);
//            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
//            Assert.AreEqual(2, builder.GridItemsPreValue.Layouts.Count);
//        }

//        [Test]
//        public static void GridViewLayouts()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            var gridDataType = builder
//                .Name("My New Grid View")
//                .AddLayout("1 column layout", 12)
//                .AddLayout("2 column layout", 4, 8)
//                .AddLayout("3 column layout", 4, 4, 4)
//                .AddLayout("4 column layout", 3, 3, 3, 3)
//                .Build();

//            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
//            Assert.AreEqual("My New Grid View", gridDataType.Name);
//            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);
//            Assert.AreEqual(4, builder.GridItemsPreValue.Layouts.Count);
//            Assert.AreEqual(1, builder.GridItemsPreValue.Layouts[0].Sections.Count);
//            Assert.AreEqual(2, builder.GridItemsPreValue.Layouts[1].Sections.Count);
//            Assert.AreEqual(3, builder.GridItemsPreValue.Layouts[2].Sections.Count);
//            Assert.AreEqual(4, builder.GridItemsPreValue.Layouts[3].Sections.Count);
//        }

//        [Test]
//        public static void GridViewKeySet()
//        {
//            var key = Guid.Parse("{7829562A-1C09-4E62-B0B5-A80E2542DDF7}");

//            var builder = new GridDataTypeTestBuilder(key)
//                .Build();

//            var gridDataType = builder
//                .Name("My New Grid View")
//                .Build();

//            Assert.AreEqual(key, gridDataType.Key);
//        }

//        [Test]
//        public static void GridViewRows()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            var gridDataType = builder
//                .Name("My New Grid View")
//                .AddRow("1 column", 12)
//                .AddRow("2 columns", 6, 6)
//                .AddRow("3 columns", 4, 4, 4)
//                .AddRow("4 columns", 3, 3, 3, 3)
//                .AddRow("5 columns", 1, 2, 3, 4, 5)
//                .Build();

//            Assert.IsInstanceOf<IDataTypeDefinition>(gridDataType);
//            Assert.AreEqual("My New Grid View", gridDataType.Name);
//            Assert.AreEqual(PropertyEditors.GridAlias, gridDataType.PropertyEditorAlias);

//            Assert.AreEqual(5, builder.GridItemsPreValue.Rows.Count);

//            Assert.AreEqual(1, builder.GridItemsPreValue.Rows[0].Areas.Count);
//            Assert.AreEqual(2, builder.GridItemsPreValue.Rows[1].Areas.Count);
//            Assert.AreEqual(3, builder.GridItemsPreValue.Rows[2].Areas.Count);
//            Assert.AreEqual(4, builder.GridItemsPreValue.Rows[3].Areas.Count);

//            Assert.AreEqual(1, builder.GridItemsPreValue.Rows[4].Areas[0].Grid);
//            Assert.AreEqual(2, builder.GridItemsPreValue.Rows[4].Areas[1].Grid);
//            Assert.AreEqual(3, builder.GridItemsPreValue.Rows[4].Areas[2].Grid);
//            Assert.AreEqual(4, builder.GridItemsPreValue.Rows[4].Areas[3].Grid);
//            Assert.AreEqual(5, builder.GridItemsPreValue.Rows[4].Areas[4].Grid);

//            Assert.IsTrue(builder.GridItemsPreValue.Rows[4].Areas[0].AllowAll);
//            Assert.IsTrue(builder.GridItemsPreValue.Rows[4].Areas[1].AllowAll);
//            Assert.IsTrue(builder.GridItemsPreValue.Rows[4].Areas[2].AllowAll);
//            Assert.IsTrue(builder.GridItemsPreValue.Rows[4].Areas[3].AllowAll);
//            Assert.IsTrue(builder.GridItemsPreValue.Rows[4].Areas[4].AllowAll);
//        }

//        [Test]
//        public static void Columns()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            builder
//                .Columns(12)
//                .Build();

//            Assert.AreEqual(12, builder.GridItemsPreValue.Columns);
//        }

//        [Test]
//        public static void InvalidColumns()
//        {
//            var builder = new GridDataTypeTestBuilder(Guid.NewGuid())
//                .Build();

//            Assert.Throws<ArgumentOutOfRangeException>(() 
//                => builder
//                    .Columns(0)
//                    .Build());
//        }
//    }
//}
