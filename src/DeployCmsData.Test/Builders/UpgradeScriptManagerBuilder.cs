using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace DeployCmsData.Test.Builders
{
    internal class UpgradeScriptManagerBuilder
    {
        readonly UpgradeScriptManager ScriptManager;
        public Mock<IUpgradeScript> UpgradeScript { get; }
        public Mock<IUpgradeLogRepository> LogRepository { get; }
        public UmbracoContextBuilder umbracoContextBuilder;

        public UpgradeScriptManagerBuilder()
        {
            UpgradeScript = new Mock<IUpgradeScript>();
            LogRepository = new Mock<IUpgradeLogRepository>();
            ScriptManager = new UpgradeScriptManager(LogRepository.Object);
            umbracoContextBuilder = new UmbracoContextBuilder();            
        }

        public UpgradeScriptManagerBuilder RunScriptReturnsTrue()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(true);
            return this;
        }

        public UpgradeScriptManagerBuilder RunScriptReturnsFalse()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(false);
            return this;
        }

        public UpgradeScriptManagerBuilder RunScriptSleeps(int milliseconds)
        {
            UpgradeScript.Setup(x => 
                x.RunScript(LogRepository.Object))
                .Callback(() => Thread.Sleep(milliseconds))
                .Returns(true);
            return this;
        }

        public UpgradeScriptManagerBuilder RunScriptThrowsException()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Throws(new Exception());
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
                    UpgradeScriptName = UpgradeScriptManager.GetScriptName(UpgradeScript.Object)
                };
                logs.Add(upgradeLog);
            }
            
            LogRepository.Setup(x => x.GetLogsByScriptName(It.IsAny<string>())).Returns(logs);

            return this;
        }

        public UpgradeScriptManagerBuilder AddRunScriptEveryTimeAttribute()
        {
            var attribute = new RunScriptEveryTimeAttribute();
            TypeDescriptor.AddAttributes(UpgradeScript.Object, attribute);

            return this;
        }

        public UpgradeScriptManager Build()
        {
            return ScriptManager;
        }

    }
}
