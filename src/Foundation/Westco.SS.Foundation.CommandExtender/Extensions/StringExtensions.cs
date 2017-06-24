using System;

namespace Sitecore.SharedSource.CommandExtender.Extensions
{
    public static class StringExtensions
    {
        public static bool Is(this string value, string compare)
        {
            return string.Compare(value, compare, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}