using System;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Core.Models
{
    public class UpgradeLog : IUpgradeLog
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UpgradeScriptName { get; set; }
        public bool Success { get; set; }
        public int RunTimeMilliseconds { get; set; }
        public string Exception { get; set; }
    }
}