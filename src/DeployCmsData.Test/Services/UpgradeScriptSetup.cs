using System;
using DeployCmsData.Services.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.Services;
using Moq;
using System.Collections.Generic;
using DeployCmsData.ActionFilters;
using System.ComponentModel;

namespace DeployCmsData.Test.Services
{
    internal class UpgradeScriptSetup
    {
        readonly UpgradeScriptManager ScriptManager;
        public Mock<IUpgradeScript> UpgradeScript { get; }
        public Mock<IUpgradeLogRepository> LogRepository { get; }
        public UmbracoContextBuilder umbracoContextBuilder;

        public UpgradeScriptSetup()
        {
            UpgradeScript = new Mock<IUpgradeScript>();
            LogRepository = new Mock<IUpgradeLogRepository>();
            ScriptManager = new UpgradeScriptManager(LogRepository.Object);
            umbracoContextBuilder = new UmbracoContextBuilder();            
        }

        public UpgradeScriptSetup RunScriptReturnsTrue()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(true);
            return this;
        }

        public UpgradeScriptSetup RunScriptReturnsFalse()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(false);
            return this;
        }

        public UpgradeScriptSetup RunScriptThrowsException()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Throws(new Exception());
            return this;
        }

        public UpgradeScriptSetup GetLogsReturnsSuccessfulLogs()
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

        public UpgradeScriptSetup AddRunScriptEveryTimeAttribute()
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
