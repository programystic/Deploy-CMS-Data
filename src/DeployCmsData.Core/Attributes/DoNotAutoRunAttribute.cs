using System;

namespace DeployCmsData.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DoNotAutoRunAttribute : Attribute
    {
    }
}
