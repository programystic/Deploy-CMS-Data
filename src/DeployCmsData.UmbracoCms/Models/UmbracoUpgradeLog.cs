﻿using System;
using DeployCmsData.Core.Interfaces;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace DeployCmsData.UmbracoCms.Models
{
    [TableName(Constants.Data.LogsTableName)]
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
        public int RuntTimeMilliseconds { get; set; }

        public UmbracoUpgradeLog()
        {
        }

        public UmbracoUpgradeLog(IUpgradeLog upgradeLog)
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