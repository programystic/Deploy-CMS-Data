using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;

namespace Integration.Web.Umb7._0.UpgradeScripts
{
    public class Templates : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var MasterTemplate = new TemplateBuilder("Master")
                .Build();

            var template = new TemplateBuilder("Home")
                .WithMasterTemplate("Master")
                .Build();

            new TemplateBuilder("TheSpecialOne")
                .WithMasterTemplate("Master")
                .Build();

            var homePageDocType = new DocumentTypeBuilder("homePage")
                .DefaultTemplate(template)
                .Update();

            return true;
        }
    }
}
