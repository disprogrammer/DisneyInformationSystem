using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Helper class to implement changes to strings.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Takes an exception and splits the type name by capital letters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Split exception type string.</returns>
        public static string ExceptionTypeStringSplit(Exception exception)
        {
            var regexCapitalLetters = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            return regexCapitalLetters.Replace(exception.GetType().Name, " ");
        }

        /// <summary>
        /// Capitalizes all the words in a string.
        /// </summary>
        /// <param name="title">String to capitalize.</param>
        /// <returns>New string.</returns>
        public static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
    }
}