using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._13.UpgradeScripts
{
    public class AllDataTypes : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            new DocumentTypeFolderBuilder("Testing").BuildAtRoot();
            
            var builder = new DocumentTypeBuilder("allDataTypes");

            builder.DefaultTab("All The Data Types");

            builder.AddField("textArea").DataType(DataType.ApprovedColor);
            builder.AddField("checkBox").DataType(DataType.CheckBox);
            builder.AddField("checkBox").DataType(DataType.CheckBoxList);
            builder.AddField("contentPicker").DataType(DataType.ContentPicker);
            builder.AddField("datePicker").DataType(DataType.DatePicker);
            builder.AddField("contentPicker").DataType(DataType.DatePickerWithTime);
            builder.AddField("dropDown").DataType(DataType.DropDown);
            builder.AddField("dropDownMultiple").DataType(DataType.DropDownMultiple);
            builder.AddField("imageCropper").DataType(DataType.ImageCropper);
            builder.AddField("label").DataType(DataType.Label);
            builder.AddField("listViewContent").DataType(DataType.ListViewContent);            
            builder.AddField("listViewMedia").DataType(DataType.ListViewMedia);
            builder.AddField("listViewMembers").DataType(DataType.ListViewMembers);
            builder.AddField("mediaPicker").DataType(DataType.MediaPicker);
            builder.AddField("memberPicker").DataType(DataType.MemberPicker);
            builder.AddField("multipleMediaPicker").DataType(DataType.MultipleMediaPicker);
            builder.AddField("numeric").DataType(DataType.Numeric);
            builder.AddField("radioBox").DataType(DataType.RadioBox);
            builder.AddField("relatedLinks").DataType(DataType.RelatedLinks);
            builder.AddField("richTextEditor").DataType(DataType.RichTextEditor);
            builder.AddField("tags").DataType(DataType.Tags);
            builder.AddField("textArea").DataType(DataType.TextArea);
            builder.AddField("textString").DataType(DataType.TextString);
            builder.AddField("upload").DataType(DataType.Upload);

            builder.BuildInFolder("Testing");

            return true;
        }
    }
}
