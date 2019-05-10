using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Services;
using DeployCmsData.UpgradeScripts_7.Constants;
using System;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class Upgrade04 : IUpgradeScript
    {
        public bool RunScript()
        {
            var gridId = new Guid("{13317E3D-C3F5-408C-9E9A-BC21E45EC8BC}");

            var library = new UmbracoLibrary();
            library.DeleteDataTypeById(gridId);
            var gridBuilder = new GridDataTypeBuilder(gridId);

            gridBuilder
                .Name(LocalDataTypes.Grid)
                .AddStandardToolBar()
                .AddStandardRows()
                .AddLayout("1 column layout", 12)
                .AddLayout("2 column layout", 4, 8)
                .AddLayout("3 column layout", 4, 4, 4)
                .AddLayout("4 column layout", 3, 3, 3, 3)
                .Build();

            return true;
        }
    }
}