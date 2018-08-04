using DeployCmsData.Services;

namespace DeployCmsData.Test
{
    class Samples
    {
        public void CreateNewDocType()
        {
            dynamic builder = new object();

            var docType = builder
                .CreateDocumentType("newAlias", "parentId")
                .Icon("icon")
                .Description("description")
                .AddTab("TabName1")
                .AddTab("TabName2")
                .AddField("fieldAlias1", "Tab1", "FieldType")
                .FieldDescription("description")
                .FieldHelp("help message")
                .AddField("fieldAlias2", "Tab2", "FieldType")
                .FieldDescription("description")
                .FieldHelp("help message")
                .SaveDocumentType();
        }

        public void UpdateDocType()
        {
            dynamic builder = new object();

            var docType = builder
                .UpdateDocumentType("alias")
                .Icon("icon")
                .AddTab("TabName")
                .Description("description")
                .AddField("fieldAlias1", "Tab1", "FieldType")
                .FieldType("type")
                .FieldDescription("description")
                .FieldHelp("help message")
                .UpdateField("fieldAlias2", "Tab2", "FieldType")
                .FieldDescription("description")
                .FieldHelp("help message")
                .Save();
        }

        public void CreateData()
        {            
        }

        public void CreateTemplate()
        {            
        }

        public void CreateDataType()
        {            
        }
    }
}
