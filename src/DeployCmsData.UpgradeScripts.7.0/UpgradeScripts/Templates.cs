using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    public class Templates : IUpgradeScript
    {
        public bool RunScript()
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
