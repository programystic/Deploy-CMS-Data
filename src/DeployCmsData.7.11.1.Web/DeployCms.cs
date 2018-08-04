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
            manager.RunScript(new Script05());
            //manager.RunScript(new Script02());
            //manager.RunScript(new Script03());
        }
    }

    public class Script05 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder(UmbracoContext.Current.Application);

            var newDocType1 =
                builder
                    .Alias("AWholeNewWorld101")
                    .Icon(Icons.Clubs)
                    .Name("This is cool")
                    .BuildInFolder("Another Folder 2", 3);

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