using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Umbraco.Core;
using DeployCmsData.Constants;
using Umbraco.Web;

namespace DeployCmsData._7_11_1.Web
{
    public class DeployCms : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var manager = new UpgradeScriptManager();
            manager.RunScriptAgain(new Script09());
        }
    }

    public class Script09 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            ClearEverythingDown();
            CreateSiteDocumentType();
            CreateFolders();
            CreatePageMetaData();
            return true;
        }

        public void ClearEverythingDown()
        {
            var library = new CmsLibrary();
            library.DeleteAllDocumentTypes();
            library.DeleteAllDocumentTypeFolders();
        }

        public void CreateSiteDocumentType()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("websiteRoot")
                .Name("Website")
                .Icon(Icons.Globe)
                .BuildAtRoot();
        }

        public void CreateFolders()
        {
            var builder = new DocumentTypeFolderBuilder();

            builder
                .Name("Page Compositions")
                .BuildAtRoot();
        }

        public void CreatePageMetaData()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("pageMetaData")
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .BuildInFolder("Page Compositions");
        }
    }

    public class Script08 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder();

            builder
                .Alias("DocTypeLevel1")
                .Icon(Icons.Clubs)
                .Name("Doc Type Level 1")
                .BuildInFolder("Folder Level 1");

            builder
                .Alias("DocTypeLevel3")
                .Icon(Icons.Clubs)
                .Name("Doc Type Level 33")
                .BuildInFolder("Folder Level 2");

            builder
                .Alias("DocTypeLevel3")
                .Icon(Icons.Clubs)
                .Name("Doc Type Level 3")
                .BuildInFolder("Folder Level 3");


            builder
                .Alias("DocTypeLevel4")
                .Icon(Icons.Clubs)
                .Name("Doc Type Level 4")
                .BuildInFolder("Folder Level 4", 4);

            return true;
        }
    }

    public class Script07 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeFolderBuilder();

            var folder1 = builder
                .Name("Folder Level 1")
                .BuildAtRoot();

            var folder2 = builder
                .Name("Folder Level 2")
                .BuildWithParentFolder("Folder Level 1");

            var folder3 = builder
                .Name("Folder Level 3")
                .BuildWithParentFolder("Folder Level 2");

            var folder4 = builder
                .Name("Folder Level 4")
                .BuildWithParentFolder("Folder Level 3");

            return true;
        }
    }
    public class Script06 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder();

            builder
                .Alias("AWholeNewWorld102")
                .Icon(Icons.Clubs)
                .Name("This is cool 2");

            builder.AddField()
                .Alias("Field 1")
                .Name("Field 1")
                .Tab("Nope")
                .DataType(CmsDataType.TextString);

            builder.BuildAtRoot();

            return true;
        }
    }

    public class Script04 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder();

            var newDocType1 =
                builder
                    .Alias("AWholeNewWorld")
                    .Icon(Icons.BatteryFull)
                    .Name("Im a so appy")
                    .BuildAtRoot();

            var newDocType2 =
                builder
                    .Alias("AWholeNewWorld 2")
                    .Icon(Icons.Factory)
                    .Name("Woooooooooooooo")
                    .BuildWithParent(newDocType1.Alias);

            //var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);
            //builder.CreateDocumentTypeAtRoot("HelloWorld", Icons.Ball, "Hello World", "Hello World", true);

            //builder.CreateDocumentTypeAtRoot()
            //    .Alias("HelloWorld")
            //    .Icon(Icons.Ball)
            //    .Name("Hello World")
            //    .AllowedAtRoot()
            //    .Build();

            return true;
        }
    }

    //public class Script02 : IUpgradeScript
    //{
    //    public bool RunScript(IUpgradeLogRepository upgradeLog)
    //    {
    //        var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);
    //        builder.CreateDocumentType("HelloWorld", "HelloWorld2", Icons.Ball, "Hello World 2", "Hello World 2", true);

    //        return true;
    //    }
    //}

    //public class Script03 : IUpgradeScript
    //{
    //    public bool RunScript(IUpgradeLogRepository upgradeLog)
    //    {
    //        var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);

    //        var rootFolder = builder.CreateFolderAtRoot("Root Folder 2");
    //        builder.CreateDocumentType(rootFolder.Id, "HelloWorld3", Icons.AlarmClock, "Hello World 3", "Hello World 3", false);

    //        var folder = builder.CreateFolder("Another Folder", "Root Folder 2", 1);
    //        builder.CreateDocumentType(folder.Id, "HelloWorld4", Icons.Ball, "Hello World 4", "Hello World 4", false);

    //        var folder2 = builder.CreateFolder("Another Folder 2", "Another Folder", 2);

    //        return true;
    //    }
    //}
}