using DeployCmsData.Services;
using DeployCmsData.Services.Interfaces;
using Umbraco.Web;

namespace DeployCmsData.UpgradeScripts
{
    public class Upgrade03 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DataTypeBuilder(UmbracoContext.Current.Application);

            return true;
        }
    }
}
