using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Umbraco.Web;

namespace DeployCmsData.Web.DeployCmsData
{
    public class Script03 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);

            var rootFolder = builder.CreateFolderAtRoot("Root Folder 2");
            builder.CreateDocumentType(rootFolder.Id, "HelloWorld3", Icons.Ball, "Hello World 3", "Hello World 3", false);

            var folder = builder.CreateFolder("Another Folder", "Root Folder 2", 1);
            builder.CreateDocumentType(folder.Id, "HelloWorld4", Icons.Ball, "Hello World 4", "Hello World 4", false);

            var folder2 = builder.CreateFolder("Another Folder 2", "Another Folder", 2);

            return true;
        }
    }
}