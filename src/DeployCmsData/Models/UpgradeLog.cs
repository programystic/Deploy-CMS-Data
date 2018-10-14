using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace DeployCmsData.Models
{
    [TableName(Constants.Data.LogsTableName)]
    [PrimaryKey(nameof(Id), autoIncrement = true)]
    public class UpgradeLog
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UpgradeScriptName { get; set; }
        public bool Success { get; set; }
        public int RuntTimeMilliseconds { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Exception { get; set; }
    }
}