using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class AllDataTypes : IUpgradeScript
    {
        public bool RunScript()
        {
            new DocumentTypeFolderBuilder("Testing").BuildAtRoot();

            var builder = new DocumentTypeBuilder("allDataTypes");

            builder.DefaultTab("All The Data Types");

            builder.AddField("textArea").DataTypeAlias(DataTypeAlias.TextArea);
            builder.AddField("checkBox").DataTypeAlias(DataTypeAlias.TrueFalse);
            builder.AddField("checkBoxList").DataTypeAlias(DataTypeAlias.CheckBoxList);
            builder.AddField("contentPicker").DataTypeAlias(DataTypeAlias.ContentPicker);
            builder.AddField("datePicker").DataTypeAlias(DataTypeAlias.DatePicker);
            builder.AddField("datePickerWithTime").DataTypeAlias(DataTypeAlias.DatePickerWithTime);
            builder.AddField("dropDown").DataTypeAlias(DataTypeAlias.DropDown);
            builder.AddField("dropDownMultiple").DataTypeAlias(DataTypeAlias.DropDownMultiple);
            builder.AddField("imageCropper").DataTypeAlias(DataTypeAlias.ImageCropper);
            builder.AddField("label").DataTypeAlias(DataTypeAlias.Label);
            builder.AddField("listViewContent").DataTypeAlias(DataTypeAlias.ListViewContent);
            builder.AddField("listViewMedia").DataTypeAlias(DataTypeAlias.ListViewMedia);
            builder.AddField("listViewMembers").DataTypeAlias(DataTypeAlias.ListViewMembers);
            builder.AddField("mediaPicker").DataTypeAlias(DataTypeAlias.MediaPicker);
            builder.AddField("memberPicker").DataTypeAlias(DataTypeAlias.MemberPicker);
            builder.AddField("multipleMediaPicker").DataTypeAlias(DataTypeAlias.MultipleMediaPicker);
            builder.AddField("numeric").DataTypeAlias(DataTypeAlias.Numeric);
            builder.AddField("radioBox").DataTypeAlias(DataTypeAlias.RadioBox);
            builder.AddField("relatedLinks").DataTypeAlias(DataTypeAlias.RelatedLinks);
            builder.AddField("richTextEditor").DataTypeAlias(DataTypeAlias.RichTextEditor);
            builder.AddField("tags").DataTypeAlias(DataTypeAlias.Tags);
            builder.AddField("textArea").DataTypeAlias(DataTypeAlias.TextArea);
            builder.AddField("textString").DataTypeAlias(DataTypeAlias.TextString);
            builder.AddField("upload").DataTypeAlias(DataTypeAlias.Upload);

            builder.BuildInFolder("Testing");

            return true;
        }
    }
}
