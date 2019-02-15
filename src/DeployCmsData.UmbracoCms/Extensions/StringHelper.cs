using System.Text.RegularExpressions;
using Umbraco.Core;

namespace DeployCmsData.UmbracoCms.Extensions
{
    public static class StringHelper
    {
        public static string AliasToName(this string value)
        {
            return Regex.Replace(value, "(\\B[A-Z])", " $1").ToFirstUpperInvariant();
        }
    }
}
