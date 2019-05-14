using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;
using static Umbraco.Core.Constants;

namespace DeployCmsData.Umbraco7.UpgradeScripts
{
    [DoNotAutoRun]
    internal class AddLegacyDataTypes : IUpgradeScript
    {
        public bool RunScript()
        {
            new DataTypeBuilder(DataTypeAlias.TrueFalse)
                .Key(DataTypeId.TrueFalse)
                .PropertyEditorAlias(PropertyEditors.TrueFalseAlias)
                .Build();

            //new DataTypeBuilder(DataTypeAlias.ContentPicker)
            //   .Key(DataTypeId.ContentPicker)
            //   .PropertyEditorAlias(PropertyEditors.ContentPickerAlias)
            //   .Build();

            //new DataTypeBuilder(DataTypeAlias.MediaPicker)
            //   .Key(DataTypeId.MediaPicker)
            //   .PropertyEditorAlias(PropertyEditors.MediaPickerAlias)
            //   .Build();

            //new DataTypeBuilder(DataTypeAlias.MemberPicker)
            //   .Key(DataTypeId.MemberPicker)
            //   .PropertyEditorAlias(PropertyEditors.MemberPickerAlias)
            //   .Build();

            //new DataTypeBuilder(DataTypeAlias.MultipleMediaPicker)
            //   .Key(DataTypeId.MultipleMediaPicker)
            //   .PropertyEditorAlias(PropertyEditors.MultipleMediaPickerAlias)
            //   .Build();

            //new DataTypeBuilder(DataTypeAlias.RelatedLinks)
            //   .Key(DataTypeId.RelatedLinks)
            //   .PropertyEditorAlias(PropertyEditors.RelatedLinksAlias)
            //   .Build();

            return true;
        }
    }
}
