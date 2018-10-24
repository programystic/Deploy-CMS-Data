using DeployCmsData.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace DeployCmsData.UmbracoCms.Models
{
    [TableName(Constants.Data.LogsTableName)]
    [PrimaryKey(nameof(Id), autoIncrement = true)]
    public class UmbracoUpgradeLog : Core.Models.UpgradeLog
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public override long Id { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public override string Exception { get; set; }

        public UmbracoUpgradeLog()
        {
        }

        public UmbracoUpgradeLog(UpgradeLog upgradeLog)
        {
            Id = upgradeLog.Id;
            Success = upgradeLog.Success;
            Timestamp = upgradeLog.Timestamp;
            UpgradeScriptName = upgradeLog.UpgradeScriptName;
            Exception = upgradeLog.Exception;
            RuntTimeMilliseconds = upgradeLog.RuntTimeMilliseconds;
        }        
    }
}