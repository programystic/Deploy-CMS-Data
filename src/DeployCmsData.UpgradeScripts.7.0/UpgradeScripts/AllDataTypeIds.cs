//using DeployCmsData.Core.Interfaces;
//using DeployCmsData.Umbraco7.Builders;
//using DeployCmsData.Umbraco7.Constants;

//namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
//{
//    class AllDataTypeIds : IUpgradeScript
//    {
//        public bool RunScript()
//        {
//            new DocumentTypeFolderBuilder("Testing2").BuildAtRoot();

//            var builder = new DocumentTypeBuilder("allDataTypes");

//            builder.DefaultTab("All The Data Types");

//            builder.AddField("textArea").DataTypeId(DataTypeId.TextArea);
//            builder.AddField("checkBox").DataTypeId(DataTypeId.TrueFalse);
//            builder.AddField("checkBoxList").DataTypeId(DataTypeId.CheckBoxList);
//            builder.AddField("contentPicker").DataTypeId(DataTypeId.ContentPicker);
//            builder.AddField("datePicker").DataTypeId(DataTypeId.DatePicker);
//            builder.AddField("datePickerWithTime").DataTypeId(DataTypeId.DatePickerWithTime);
//            builder.AddField("dropDown").DataTypeId(DataTypeId.DropDown);
//            builder.AddField("dropDownMultiple").DataTypeId(DataTypeId.DropDownMultiple);
//            builder.AddField("imageCropper").DataTypeId(DataTypeId.ImageCropper);
//            builder.AddField("label").DataTypeId(DataTypeId.Label);
//            builder.AddField("listViewContent").DataTypeId(DataTypeId.ListViewContent);
//            builder.AddField("listViewMedia").DataTypeId(DataTypeId.ListViewMedia);
//            builder.AddField("listViewMembers").DataTypeId(DataTypeId.ListViewMembers);
//            builder.AddField("mediaPicker").DataTypeId(DataTypeId.MediaPicker);
//            builder.AddField("memberPicker").DataTypeId(DataTypeId.MemberPicker);
//            builder.AddField("multipleMediaPicker").DataTypeId(DataTypeId.MultipleMediaPicker);
//            builder.AddField("numeric").DataTypeId(DataTypeId.Numeric);
//            builder.AddField("radioBox").DataTypeId(DataTypeId.RadioBox);
//            builder.AddField("relatedLinks").DataTypeId(DataTypeId.RelatedLinks);
//            builder.AddField("richTextEditor").DataTypeId(DataTypeId.RichTextEditor);
//            builder.AddField("tags").DataTypeId(DataTypeId.Tags);
//            builder.AddField("textArea").DataTypeId(DataTypeId.TextArea);
//            builder.AddField("textString").DataTypeId(DataTypeId.TextString);
//            builder.AddField("upload").DataTypeId(DataTypeId.Upload);

//            builder.BuildInFolder("Testing2");

//            return true;
//        }
//    }
//}
