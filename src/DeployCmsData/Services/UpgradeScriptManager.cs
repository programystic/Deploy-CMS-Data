using System;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using Umbraco.Core;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Services
{
    public sealed class UpgradeScriptManager : IDisposable
    {
        private readonly IUpgradeLogRepository _logDatastore;

        public UpgradeScriptManager()
        {
            _logDatastore = new UpgradeLogRepository();
        }

        public UpgradeScriptManager(IUpgradeLogRepository logDataStore)
        {
            _logDatastore = logDataStore;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logDatastore.DisposeIfDisposable();
            }
        }

        public UpgradeLog RunScript(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                throw new ArgumentNullException(nameof(upgradeScript));

            if (ScriptAlreadyRun(upgradeScript))
                return null;

            var start = DateTime.Now;
            var result = upgradeScript.RunScript(_logDatastore);

            var upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = upgradeScript.GetType().FullName,
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Success = result,
                RuntTimeMilliseconds = (DateTime.Now - start).Milliseconds
            };

            _logDatastore.SaveLog(upgradeLog);
            return upgradeLog;
        }

        private bool ScriptAlreadyRun(IUpgradeScript upgradeScript)
        {
            var upgradeScriptName = upgradeScript.GetType().FullName;
            var result = _logDatastore.GetLog(upgradeScriptName);

            return result?.UpgradeScriptName == upgradeScriptName;
        }

    }
}
