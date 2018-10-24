using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade03 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            //var builder = new DataTypeBuilder(UmbracoContext.Current.Application);

            return true;
        }
    }
}
