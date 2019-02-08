using System;

namespace DeployCmsData.Core.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DoNotAutoRunAttribute : Attribute
    {
    }
}
