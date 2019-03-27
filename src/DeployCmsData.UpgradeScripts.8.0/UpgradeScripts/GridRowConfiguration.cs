using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using System;

namespace DeployCmsData.UpgradeScripts._8._0.UpgradeScripts
{
    [DoNotAutoRun]
    public class GridRowConfiguration : IUpgradeScript
    {
        public bool RunScript()
        {
            new GridDataTypeBuilder(new Guid("{B0F71D78-745E-4BE5-925E-FA087D44C919}"))
                .AddAreaToRow("row 1", 2)
                .AddAreaToRow("row 1", 4)
                .AddAreaToRow("row 1", 6)
                .Build();

            new GridDataTypeBuilder(new Guid("{9447F9CF-2112-45CD-933B-6F8F6411EF65}"))
                .AddRow("row 1", 2, 4)
                .AddAreaToRow("row 1", 6)
                .Build();

            new GridDataTypeBuilder(new Guid("{D35416BC-58FC-4DB3-B67E-B7111BDA8CAA}"))
                .AddAreaToRow("row 1", 2, 10)
                .AddAreaToRow("row 1", 4, 20)
                .AddAreaToRow("row 1", 6, 30)
                .Build();

            new GridDataTypeBuilder(new Guid("{4E3F4D54-3900-43A7-B37F-ABDA1802631E}"))
                .AddAreaToRow("row 1", 2, 10, "rte", "headline", "image")
                .AddAreaToRow("row 1", 4, 20, "image")
                .AddAreaToRow("row 1", 6, 30, "headline", "rte")
                .Build();

            return true;
        }
    }
}
