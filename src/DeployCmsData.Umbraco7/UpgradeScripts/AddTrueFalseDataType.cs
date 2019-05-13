using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;
using static Umbraco.Core.Constants;

namespace DeployCmsData.Umbraco7.UpgradeScripts
{
    [DoNotAutoRun]
    internal class AddTrueFalseDataType : IUpgradeScript
    {
        public bool RunScript()
        {
            new DataTypeBuilder(DataTypeAlias.TrueFalse)
                .PropertyEditorAlias(PropertyEditors.TrueFalseAlias)
                .Build();

            return true;
        }
    }
}
