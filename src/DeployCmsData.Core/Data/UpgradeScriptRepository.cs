using System;
using System.Collections.Generic;
using System.Linq;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Core.Data
{
    public class UpgradeScriptRepository : IUpgradeScriptRepository
    {
        IEnumerable<Type> IUpgradeScriptRepository.GetTypes =>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
    }
}
