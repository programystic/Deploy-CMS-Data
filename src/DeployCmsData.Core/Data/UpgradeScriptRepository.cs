using System;
using System.Collections.Generic;
using System.Linq;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Core.Data
{
    public sealed class UpgradeScriptRepository : IUpgradeScriptRepository
    {
        IEnumerable<Type> IUpgradeScriptRepository.GetTypes =>
            GetTypes;

        public static IEnumerable<Type> GetTypes =>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
    }
}
