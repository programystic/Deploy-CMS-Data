using DeployCmsData.Core.Interfaces;
using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Validation;

namespace DeployCmsData.UmbracoCms.Models
{
    [TableName(Constants.Database.LogsTableName)]
    [PrimaryKey(nameof(Id), autoIncrement = true)]
    public class UmbracoUpgradeLog : IUpgradeLog
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public long Id { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Exception { get; set; }

        public DateTime Timestamp { get; set; }
        public string UpgradeScriptName { get; set; }
        public bool Success { get; set; }
        public int RunTimeMilliseconds { get; set; }

        public UmbracoUpgradeLog()
        {
        }

        public UmbracoUpgradeLog(IUpgradeLog upgradeLog)
        {
            Requires.NotNull(upgradeLog, nameof(upgradeLog));

            Id = upgradeLog.Id;
            Success = upgradeLog.Success;
            Timestamp = upgradeLog.Timestamp;
            UpgradeScriptName = upgradeLog.UpgradeScriptName;
            Exception = upgradeLog.Exception;
            RunTimeMilliseconds = upgradeLog.RunTimeMilliseconds;
        }
    }
}