using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Services;
using System;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [RunScriptEveryTime]
    public class MultiNodeTreePicker : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var id = Guid.Parse("{60FFCA99-3B98-49EA-9F64-E4C69BB00285}");

            Library.DeleteDataTypeById(id);
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
