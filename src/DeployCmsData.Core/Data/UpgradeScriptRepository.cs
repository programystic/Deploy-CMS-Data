using System;
using System.Collections.Generic;
using System.Linq;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Core.Data
{
    public class UpgradeScriptRepository : IUpgradeScriptRepository
    {
        public IEnumerable<Type> GetTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
             .SelectMany(s => s.GetTypes());
        }
    }
}
