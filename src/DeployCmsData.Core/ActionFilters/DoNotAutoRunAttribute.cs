using System;

namespace DeployCmsData.Core.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DoNotAutoRunAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
    }
}
