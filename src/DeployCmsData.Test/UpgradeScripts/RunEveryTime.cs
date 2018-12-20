﻿using System;
using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    [RunScriptEveryTime]
    public class RunEveryTime : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}