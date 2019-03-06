using DeployCmsData.Core.Interfaces;
using System;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    public class Fails : IUpgradeScript
    {
        public bool RunScript()
        {
            throw new InvalidProgramException();
        }
    }
}