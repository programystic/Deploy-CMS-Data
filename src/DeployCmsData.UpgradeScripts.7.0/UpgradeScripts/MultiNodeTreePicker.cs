using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Services;
using System;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class MultiNodeTreePicker : IUpgradeScript
    {
        public bool RunScript()
        {
            var id = Guid.Parse("{60FFCA99-3B98-49EA-9F64-E4C69BB00285}");
            var library = new UmbracoLibrary();

            library.DeleteDataTypeById(id);
            var builder = new MultiNodeTreePickerBuilder(id);

            builder
                .Name("Another Multi Node Tree Picker")
                .AllowItemsOfType("type1", "type2")
                .MinimumNumberOfItems(1)
                .MaximumNumberOfItems(5)
                .ShowOpenButton()
                //.StartNodeContent(1234)
                .Build();

            return true;
        }
    }
}
