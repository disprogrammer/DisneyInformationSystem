using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Random Generator class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RandomGenerator
    {
        /// <summary>
        /// Creates a random pin and checks that it does not already exist in the database table.
        /// </summary>
        public static string RetrieveRandomGeneratedPin(List<string> listOfPins)
        {
            var pinAlreadyExists = false;
            var generatedPin = string.Empty;

            while (!pinAlreadyExists)
            {
                generatedPin = GenerateRandomPin();
                if (listOfPins.Contains(generatedPin))
                {
                    continue;
                }

                pinAlreadyExists = true;
            }

            return generatedPin;
        }

        /// <summary>
        /// Generates a random number for an admin's or a user's PIN as the primary key in the database.
        /// </summary>
        /// <returns>Ten digit number string.</returns>
        private static string GenerateRandomPin()
        {
            var random = new Random();
            return random.Next(0000000000, 999999999).ToString("D10");
        }
    }
}