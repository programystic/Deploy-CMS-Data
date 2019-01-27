using System;
using System.Globalization;

namespace DeployCmsData.Core.Extensions
{
    public static class StringFormat
    {
        public static string ToInvariant(FormattableString formattableString)
        {
            if (formattableString == null)
            {
                throw new ArgumentNullException(nameof(formattableString));
            }

            return formattableString.ToString(CultureInfo.InvariantCulture);
        }
    }
}
