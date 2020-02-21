namespace AirHockey.Utility.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        public static bool EndsWithAny(
            this string str,
            string[] candidates,
            StringComparison comparison = StringComparison.CurrentCulture)
        {
            return candidates.Any(candidate => str.EndsWith(candidate, comparison));
        }
    }
}
