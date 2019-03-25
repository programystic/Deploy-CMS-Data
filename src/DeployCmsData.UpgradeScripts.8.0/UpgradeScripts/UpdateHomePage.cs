using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using System;

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

            builder.AddField("multiNodeTreePicker")
                .DataType(Guid.Parse("{60FFCA99-3B98-49EA-9F64-E4C69BB00285}"));

            builder
                .AllowedAsRoot()
                .Update();

            return true;
        }
    }
}
