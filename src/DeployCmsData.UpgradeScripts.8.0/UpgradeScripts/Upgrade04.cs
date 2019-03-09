using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Services;
using DeployCmsData.UpgradeScripts_8.Constants;

namespace DeployCmsData.UpgradeScripts_8.UpgradeScripts
{
    [DoNotAutoRun]
    public class Upgrade04 : IUpgradeScript
    {
        public bool RunScript()
        {
            //var library = new UmbracoLibrary();
            //library.DeleteDataTypeById(LocalDataTypes.Grid);
            //var gridBuilder = new GridDataTypeBuilder(LocalDataTypes.Grid);

            //gridBuilder
            //    .Name("Another Grid View")
            //    .AddStandardToolBar()
            //    .AddStandardRows()
            //    .AddLayout("1 column layout", 12)
            //    .AddLayout("2 column layout", 4, 8)
            //    .AddLayout("3 column layout", 4, 4, 4)
            //    .AddLayout("4 column layout", 3, 3, 3, 3)
            //    .Build();

            return true;
        }
    }
}