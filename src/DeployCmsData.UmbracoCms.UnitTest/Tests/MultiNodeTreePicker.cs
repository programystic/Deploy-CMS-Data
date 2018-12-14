using System;
using DeployCmsData.UmbracoCms.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    internal class MultiNodeTreePicker
    {
        [Test]
        public static void CreateSimplePicker()
        {
            var dataTypeService = new Mock<IDataTypeService>();
            var builder = new MultiNodeTreePickerBuilder(dataTypeService.Object, Guid.NewGuid());

            var dataType = builder
                .Name("My New Tree Picker")
                .NodeType(Constants.MultiNodeTreePickerNodeType.Content)
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(1)
                .MaximumNumberOfItems(5)
                .ShowOpenButton()
                .Build();
        }
    }
}
