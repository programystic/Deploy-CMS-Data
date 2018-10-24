using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade04 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {            
            var gridBuilder = new GridDataTypeBuilder(UmbracoContext.Current.Application.Services.DataTypeService);

            gridBuilder
                .DeleteGrid("Another Grid View")
                .Name("Another Grid View")
                .AddStandardToolbar()
                .AddStandardLayouts()
                .AddTemplate("1 column layout", 12)
                .AddTemplate("2 column layout", 4, 8)
                .AddTemplate("3 column layout", 4, 4, 4)
                .AddTemplate("4 column layout", 3, 3, 3, 3)
                .Build();

            return true;
        }
    }
}