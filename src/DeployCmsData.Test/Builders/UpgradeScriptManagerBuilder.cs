using System;
using System.Collections.Generic;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.Core.Services;
using Moq;
using static System.FormattableString;

namespace DeployCmsData.Test.Builders
{
    internal class UpgradeScriptManagerBuilder
    {        
        public Mock<IUpgradeLogRepository> LogRepository { get; }
        public Mock<IUpgradeScriptRepository> UpgradeScriptRepository { get; }
        readonly UpgradeScriptManager ScriptManager;
        readonly List<Type> Types;

        public UpgradeScriptManagerBuilder()
        {            
            LogRepository = new Mock<IUpgradeLogRepository>();
            UpgradeScriptRepository = new Mock<IUpgradeScriptRepository>();

            Types = new List<Type>();
            UpgradeScriptRepository.Setup(x => x.GetTypes).Returns(Types);

            ScriptManager = new UpgradeScriptManager(LogRepository.Object, UpgradeScriptRepository.Object);
        }

        public UpgradeScriptManagerBuilder RunScriptReturnsTrue()
        {            
            return this;
        }

        public UpgradeScriptManagerBuilder GetLogsReturnsSuccessfulLogs()
        {
            var logs = new List<UpgradeLog>();

            for (int i = 0; i < 10; i++)
            {
                var upgradeLog = new UpgradeLog
                {
                    Id = i,
                    Success = true,
                    UpgradeScriptName = Invariant($"UpgradeScript{i}")
                };
                logs.Add(upgradeLog);
            }
            
            LogRepository.Setup(x => x.GetLogsByScriptName(It.IsAny<string>())).Returns(logs);

            return this;
        }

        public UpgradeScriptManagerBuilder AddScript(Type upgradeScript)
        {
            Types.Add(upgradeScript);            

            return this;
        }

        public UpgradeScriptManager Build()
        {            
            return ScriptManager;
        }

    }
}