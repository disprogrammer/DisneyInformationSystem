using DisneyInformationSystem.Business.Database.Records;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Helper class to implement changes to strings.
    /// </summary>
    public static partial class StringHelper
    {
        /// <summary>
        /// Takes an exception and splits the type name by capital letters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Split exception type string.</returns>
        [SuppressMessage("GeneratedRegex", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Causes a Sonar error.")]
        public static string SplitObjectsAndPropertiesWords(string name)
        {
            var splitWords = Regex.Split(name, @"(?<!^)(?=[A-Z])");
            return string.Join(" ", splitWords).Trim();
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

        /// <summary>
        /// Gets the person signed in and creates string for the console title.
        /// </summary>
        /// <returns>Person signed in string.</returns>
        public static string PersonTitleString(Person personSignedIn)
        {
            string signInTitleString = null;
            if (personSignedIn == null)
            {
                return "No one signed in.";
            }
            else if (personSignedIn.PIN.StartsWith("A"))
            {
                Admin admin = (Admin)personSignedIn;
                signInTitleString = $"{admin.PIN}: {admin.FirstName} {admin.LastName}";
            }
            else if (personSignedIn.PIN.StartsWith("U"))
            {
                User user = (User)personSignedIn;
                signInTitleString = $"{user.PIN}: {user.FirstName} {user.LastName}";
            }

            return signInTitleString;
        }
    }
}