using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;
using DeployCmsData.UpgradeScripts_7.Constants;
using System;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class UpdateHomePage : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder = new DocumentTypeBuilder("homePage");

            builder.AddField("additionalContent")
                .DataTypeAlias(DataTypeAlias.RichTextEditor);

            builder.AddField("multiNodeTreePicker")
                .DataTypeAlias(LocalDataTypes.MultiNodeTreePicker);

            builder
                .AllowedAsRoot()
                .Update();

            return true;
        }
    }
}
