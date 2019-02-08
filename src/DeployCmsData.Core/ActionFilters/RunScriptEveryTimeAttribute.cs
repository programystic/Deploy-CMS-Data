using System;

namespace DeployCmsData.Core.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RunScriptEveryTimeAttribute : Attribute
    {
    }
}
