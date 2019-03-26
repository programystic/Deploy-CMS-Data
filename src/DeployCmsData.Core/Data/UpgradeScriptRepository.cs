using DeployCmsData.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DeployCmsData.Core.Data
{
    public sealed class UpgradeScriptRepository : IUpgradeScriptRepository
    {
        IEnumerable<Type> IUpgradeScriptRepository.GetTypes =>
            GetTypes;

        private IEnumerable<Type> GetTypes
        {
            get
            {
                var result = new List<Type>();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var assemblyTypes = assembly.GetTypes();
                        result.AddRange(assemblyTypes);
                    }
                    catch
                    {
                        // there was an error calling assembly.GetTypes() - there's nothing we can do 
                    }
                }

                return result;
            }
        }
    }
}
