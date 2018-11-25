using System;
using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade04 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var gridBuilder = new GridDataTypeBuilder();
            var id = Guid.Parse("{3B7F4064-E61E-4937-BFE8-3FFF0C71977A}");

            gridBuilder
                .DeleteGrid(id)
                .Name("Another Grid View")
                .Key(id)
                .AddStandardToolbar()
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