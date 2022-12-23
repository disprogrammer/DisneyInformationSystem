﻿using DisneyInformationSystem.Business.Database.Records;
using System;
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
        public static string SplitObjectsAndPropertiesWords(string name)
        {
            var regexCapitalLetters = RetrievesCapitalWordsFromString();
            return regexCapitalLetters.Replace(name, " ");
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

        /// <summary>
        /// Retrieves the words with capital letters.
        /// </summary>
        /// <returns>String of capital words.</returns>
        [GeneratedRegex("(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace)]
        private static partial Regex RetrievesCapitalWordsFromString();
    }
}