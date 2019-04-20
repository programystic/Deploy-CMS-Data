using System;

namespace DeployCmsData.Umbraco7.Constants
{
    public static class DataType
    {
        public static Guid ListViewMembers { get; } = new Guid("AA2C52A0-CE87-4E65-A47C-7DF09358585D");
        public static Guid ListViewMedia { get; } = new Guid("3A0156C4-3B8C-4803-BDC1-6871FAA83FFF");
        public static Guid ListViewContent { get; } = new Guid("C0808DD3-8133-4E4B-8CE8-E2BEA84A96A4");
        public static Guid Label { get; } = new Guid("F0BC4BFB-B499-40D6-BA86-058885A5178C");
        public static Guid Upload { get; } = new Guid("84C6B441-31DF-4FFE-B67E-67D5BC3AE65A");
        public static Guid TextArea { get; } = new Guid("C6BAC0DD-4AB9-45B1-8E30-E4B619EE5DA3");
        public static Guid TextString { get; } = new Guid("0CC0EBA1-9960-42C9-BF9B-60E150B429AE");
        public static Guid RichTextEditor { get; } = new Guid("CA90C950-0AFF-4E72-B976-A30B1AC57DAD");
        public static Guid CheckBox { get; } = new Guid("92897BC6-A5F3-4FFE-AE27-F2E7E33DDA49");
        public static Guid CheckBoxList { get; } = new Guid("FBAF13A8-4036-41F2-93A3-974F678C312A");
        public static Guid DropDown { get; } = new Guid("0B6A45E7-44BA-430D-9DA5-4E46060B9E03");
        public static Guid DatePicker { get; } = new Guid("5046194E-4237-453C-A547-15DB3A07C4E1");
        public static Guid RadioBox { get; } = new Guid("BB5F57C9-CE2B-4BB9-B697-4CACA783A805");
        public static Guid DropDownMultiple { get; } = new Guid("F38F0AC7-1D27-439C-9F3F-089CD8825A53");
        public static Guid ApprovedColor { get; } = new Guid("0225AF17-B302-49CB-9176-B9F35CAB9C17");
        public static Guid DatePickerWithTime { get; } = new Guid("E4D66C0F-B935-4200-81F0-025F7256B89A");
        public static Guid Tags { get; } = new Guid("B6B73142-B9C1-4BF8-A16D-E1C23320B549");
        public static Guid ImageCropper { get; } = new Guid("1DF9F033-E6D4-451F-B8D2-E0CBC50A836F");
        public static Guid ContentPicker { get; } = new Guid("FD1E0DA5-5606-4862-B679-5D0CF3A52A59");
        public static Guid MemberPicker { get; } = new Guid("1EA2E01F-EBD8-4CE1-8D71-6B1149E63548");
        public static Guid MediaPicker { get; } = new Guid("135D60E0-64D9-49ED-AB08-893C9BA44AE5");
        public static Guid MultipleMediaPicker { get; } = new Guid("9DBBCBBB-2327-434A-B355-AF1B84E5010A");
        public static Guid RelatedLinks { get; } = new Guid("B4E3535A-1753-47E2-8568-602CF8CFEE6F");
        public static Guid Numeric { get; set; } = new Guid("2E6D3631-066E-44B8-AEC4-96F09099B2B5");
    }
}
