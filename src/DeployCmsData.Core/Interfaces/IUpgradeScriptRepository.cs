using System;
using System.Collections.Generic;

namespace DeployCmsData.Core.Interfaces
{
    public interface IUpgradeScriptRepository
    {
        IEnumerable<Type> GetTypes();
    }
}
