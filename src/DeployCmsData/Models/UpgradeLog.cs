using System;

namespace DeployCmsData.Models
{
    public class UpgradeLog
    {
        public virtual long Id { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual string UpgradeScriptName { get; set; }
        public virtual bool Success { get; set; }
        public virtual int RuntTimeMilliseconds { get; set; }
        public virtual string Exception { get; set; }
    }
}