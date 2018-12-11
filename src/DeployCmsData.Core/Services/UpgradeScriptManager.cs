using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Constants;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Core.Services
{
    public sealed class UpgradeScriptManager
    {
        public IUpgradeLogRepository LogDataStore { get; }
        public IUpgradeScriptRepository UpgradeScriptRepository { get; }

        public UpgradeScriptManager(IUpgradeLogRepository logDataStore, IUpgradeScriptRepository upgradeScriptRepository)
        {
            LogDataStore = logDataStore;
            UpgradeScriptRepository = upgradeScriptRepository;
        }

        public IUpgradeLog RunScriptIfNeeded(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
            {
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };
            }

            if (ScriptHasAlreadyRunWithSuccess(upgradeScript) && ScriptShouldntBeRunEveryTime(upgradeScript))
            {
                return null;
            }

            return RunScript(upgradeScript);
        }

        // We need a catch-all exception here as we don't want to raise an exception during startup.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public UpgradeLog RunScript(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
            {
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };
            }

            UpgradeLog upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = GetScriptName(upgradeScript),
                Timestamp = DateTime.Now
            };

            DateTime start = DateTime.Now;

            try
            {
                upgradeLog.Success = upgradeScript.RunScript(LogDataStore);
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
            LogDataStore.SaveLog(upgradeLog);
            return upgradeLog;
        }

        private bool ScriptHasAlreadyRunWithSuccess(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
            {
                return false;
            }

            string scriptName = GetScriptName(upgradeScript);
            IEnumerable<IUpgradeLog> logs = LogDataStore.GetLogsByScriptName(scriptName);
            bool atLeastOneSuccessfulLog = logs.Any(x => x.Success);

            return atLeastOneSuccessfulLog;
        }

        private bool ScriptShouldntBeRunEveryTime(IUpgradeScript upgradeScript)
        {
            return !ScriptHasAttribute<RunScriptEveryTimeAttribute>(upgradeScript);
        }

        private static bool ScriptHasAttribute<T>(IUpgradeScript upgradeScript)
        {
            IEnumerable<T> attributes = TypeDescriptor
                .GetAttributes(upgradeScript)
                .OfType<T>();

            return attributes.Any();
        }

        public static string GetScriptName(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
            {
                throw new ArgumentNullException(nameof(upgradeScript));
            }

            return upgradeScript.GetType().FullName;
        }

        public int RunAllScriptsIfNeeded()
        {
            int scriptRunCount = 0;

            foreach (IUpgradeScript script in Scripts)
            {
                IUpgradeLog result = RunScriptIfNeeded(script);
                if (result != null && result.Success)
                {
                    scriptRunCount++;
                }
            }

            return scriptRunCount;
        }

        public IEnumerable<IUpgradeScript> Scripts
        {
            get
            {
                List<IUpgradeScript> scripts = new List<IUpgradeScript>();
                Type type = typeof(IUpgradeScript);

                List<Type> types = UpgradeScriptRepository
                    .GetTypes
                    .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
                    .OrderBy(x => x.Name)
                    .ToList();

                foreach (Type scriptType in types)
                {
                    IUpgradeScript script = (IUpgradeScript)Activator.CreateInstance(scriptType);
                    if (!ScriptHasAttribute<DoNotAutoRunAttribute>(script))
                    {
                        scripts.Add(script);
                    }
                }

                return scripts;
            }
        }
    }
}