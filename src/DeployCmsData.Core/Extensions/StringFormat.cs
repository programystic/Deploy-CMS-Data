using System;
using System.Globalization;
using Validation;

namespace DeployCmsData.Core.Extensions
{
    public static class StringFormat
    {
        public static string ToInvariant(FormattableString formattableString)
        {
            Requires.NotNull(formattableString, nameof(formattableString));

            return formattableString.ToString(CultureInfo.InvariantCulture);
        }
    }
}
