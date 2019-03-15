using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace DeployCmsData.UpgradeScripts_8.UpgradeScripts
{
    [DoNotAutoRun]
    public class UpdateHomePage : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder = new DocumentTypeBuilder("homePage");

            builder.AddField("additionalContent")
                .DataType(DataType.RichTextEditor);

            builder
                .AllowedAsRoot()
                .Update();

            return true;
        }
    }
}
