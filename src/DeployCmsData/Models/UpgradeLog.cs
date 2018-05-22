using System;

namespace DeployCmsData.Models
{
    public class UpgradeLog
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UpgradeScriptName { get; set; }
        public bool Success { get; set; }
        public int RuntTimeMilliseconds { get; set; }
    }
}
