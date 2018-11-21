using System;

namespace DeployCmsData.Core.Interfaces
{
    public interface IUpgradeLog
    {
        long Id { get; set; }
        DateTime Timestamp { get; set; }
        string UpgradeScriptName { get; set; }
        bool Success { get; set; }
        int RuntTimeMilliseconds { get; set; }
        string Exception { get; set; }
    }
}