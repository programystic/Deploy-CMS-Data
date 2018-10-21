using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Constants;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Core.Services
{
    public sealed class UpgradeScriptManager
    {
        public readonly IUpgradeLogRepository LogDatastore;

        public UpgradeScriptManager(IUpgradeLogRepository logDataStore)
            => LogDatastore = logDataStore;

        public UpgradeLog RunScriptIfNeeded(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            if (ScriptHasAlreadyRunWithSuccess(upgradeScript) && ScriptShouldntBeRunEveryTime(upgradeScript))
                return null;

            return RunScript(upgradeScript);
        }

        // We need a catch-all exception here as we don't want to raise an exception during startup.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public UpgradeLog RunScript(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            var upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = GetScriptName(upgradeScript),
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
                upgradeLog.Exception =
                    e.Message
                    + Environment.NewLine
                    + e.Source
                    + Environment.NewLine
                    + e.StackTrace;

                Console.WriteLine(e);
            }

            upgradeLog.RuntTimeMilliseconds = (DateTime.Now - start).Milliseconds;
            LogDatastore.SaveLog(upgradeLog);
            return upgradeLog;
        }

        private bool ScriptHasAlreadyRunWithSuccess(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null) return false;
            var scriptName = GetScriptName(upgradeScript);
            var logs = LogDatastore.GetLogsByScriptName(scriptName);
            var atLeastOneSuccessfulLog = logs.Any(x => x.Success);

            return atLeastOneSuccessfulLog;
        }

        private bool ScriptShouldntBeRunEveryTime(IUpgradeScript upgradeScript)
        {
            return !ScriptHasAttribute<RunScriptEveryTimeAttribute>(upgradeScript);
        }

        private bool ScriptHasAttribute<T>(IUpgradeScript upgradeScript)
        {
            var attributes = TypeDescriptor
                .GetAttributes(upgradeScript)
                .OfType<T>();

            return attributes.Any();
        }

        public static string GetScriptName(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                throw new ArgumentNullException(nameof(upgradeScript));

            return upgradeScript.GetType().FullName;
        }

        public void RunAllScriptsIfNeeded()
        {
            foreach (var script in GetAllScripts())
            {
                RunScriptIfNeeded(script);
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