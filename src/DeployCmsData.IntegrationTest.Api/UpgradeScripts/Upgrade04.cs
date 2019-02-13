using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Services;
using System;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade04 : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var id = Guid.Parse("{3B7F4064-E61E-4937-BFE8-3FFF0C71977A}");

            Library.DeleteDataTypeById(id);
            var gridBuilder = new GridDataTypeBuilder(id);

            gridBuilder
                .Name("Another Grid View")
                .AddStandardToolBar()
                .AddStandardRows()
                .AddLayout("1 column layout", 12)
                .AddLayout("2 column layout", 4, 8)
                .AddLayout("3 column layout", 4, 4, 4)
                .AddLayout("4 column layout", 3, 3, 3, 3)
                .Build();

            return true;
        }
    }
}