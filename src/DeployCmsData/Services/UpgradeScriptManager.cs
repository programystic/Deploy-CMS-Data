using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Services
{
    public sealed class UpgradeScriptManager
    {
        public readonly IUpgradeLogRepository LogDatastore;

        public UpgradeScriptManager(IUpgradeLogRepository logDataStore)
        {
            LogDatastore = logDataStore;
        }

        public UpgradeLog RunScript(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            if (ScriptAlreadyRun(upgradeScript))
                return null;

            return RunScriptAgain(upgradeScript);
        }

        // We need a catch-all exception here as we don't want to raise an exception during startup.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public UpgradeLog RunScriptAgain(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            var upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = GetScriptName(upgradeScript),
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };

            var start = DateTime.Now;
            try
            {
                upgradeLog.Success = upgradeScript.RunScript(LogDatastore);
            }
            catch (Exception e)
            {
                upgradeLog.Success = false;
                upgradeLog.Exception = e.Message;
                Console.WriteLine(e);
            }

            upgradeLog.RuntTimeMilliseconds = (DateTime.Now - start).Milliseconds;
            LogDatastore.SaveLog(upgradeLog);
            return upgradeLog;
        }

        private bool ScriptAlreadyRun(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null) return false;

            var scriptName = GetScriptName(upgradeScript);
            var result = LogDatastore.GetLog(scriptName);

            return result?.UpgradeScriptName == scriptName;
        }

        public static string GetScriptName(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                throw new ArgumentNullException(nameof(upgradeScript));

            return upgradeScript.GetType().FullName;
        }

        public void RunAllScripts()
        {
            foreach (var script in GetAllScripts())
            {
                script.RunScript(LogDatastore);
            }
        }

        public IEnumerable<IUpgradeScript> GetAllScripts()
        {
            var scripts = new List<IUpgradeScript>();
            var type = typeof(IUpgradeScript);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
                .OrderBy(x => x.Name);

            foreach (var scriptType in types)
            {
                var script = (IUpgradeScript)Activator.CreateInstance(scriptType);
                scripts.Add(script);
            }
                
            return scripts;
        }
    }
}