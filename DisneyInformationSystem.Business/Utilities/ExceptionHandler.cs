using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Exception Handler class.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Checks if the email address contains the address sign (@).
        /// </summary>
        /// <param name="emailAddress">Email address.</param>
        public static void CheckIfEmailContainsAddressSign(string emailAddress)
        {
            if (!emailAddress.Contains('@'))
            {
                throw new AddressSignNotFoundException($"{emailAddress} does not contain '@'. Please try again.");
            }
        }

        /// <summary>
        /// Checks if email is already in the database.
        /// </summary>
        /// <param name="emailAddress">Email address.</param>
        /// <param name="listOfEmailAddresses">List of email addresses.</param>
        public static void CheckThatEmailDoesNotAlreadyExist(string emailAddress, List<string> listOfEmailAddresses)
        {
            if (listOfEmailAddresses.Contains(emailAddress))
            {
                throw new EmailAlreadyExistsException("Your inputted email address already exists.");
            }
        }

        /// <summary>
        /// Checks if the admin type code inputted is valid.
        /// </summary>
        /// <param name="adminTypeCode">Admin Type Code.</param>
        /// <param name="listOfAdminTypes">List of admin types.</param>
        public static void CheckIfAdminTypeCodeIsValid(string adminTypeCode, List<AdminTypes> listOfAdminTypes)
        {
            if (adminTypeCode.Length < 3 || adminTypeCode.Length > 3)
            {
                throw new AdminTypeInvalidException("Admin Type Code given is less than or greater than three characters long.");
            }

            if (!adminTypeCode.All(char.IsLetter))
            {
                throw new AdminTypeInvalidException("Code entered contains a character that is not a letter.");
            }

            if (!listOfAdminTypes.Any(adminType => adminType.ID == adminTypeCode))
            {
                throw new AdminTypeInvalidException("Code provided is not found in our database.");
            }
        }

        /// <summary>
        /// Checks if the phone number provided is valid.
        /// </summary>
        /// <param name="phoneNumber">Phone number.</param>
        public static void CheckIfPhoneNumberIsValid(string phoneNumber)
        {
            if (!phoneNumber.All(char.IsDigit))
            {
                throw new PhoneNumberInvalidException("Phone number should consist only of digits.");
            }
            else if (phoneNumber.Length != 10)
            {
                throw new PhoneNumberInvalidException("Phone number cannot be more than 10 digits.");
            }
        }

        /// <summary>
        /// Checks if the resort acronym is valid.
        /// </summary>
        /// <param name="acronym">Acronym string.</param>
        public static void CheckIfAcronymIsValid(string acronym)
        {
            if (!acronym.All(char.IsLetter))
            {
                throw new AcronymInvalidException("The resort acronym should consist only letters.");
            }

            if (acronym.Length > 3 || acronym.Length < 3)
            {
                throw new AcronymInvalidException("The resort acronym is less than or greater than three letters long.");
            }
        }

        /// <summary>
        /// Checks if the input provided is a number.
        /// </summary>
        /// <param name="input">Input string.</param>
        public static int CheckIfInputIsNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            if (!input.All(char.IsDigit))
            {
                throw new FormatException("The input provided was not a number.");
            }

            return Convert.ToInt32(input);
        }

        /// <summary>
        /// Checks if the date string is a valid date.
        /// </summary>
        /// <param name="dateString">Date string.</param>
        /// <returns>Date time if no exception is thrown.</returns>
        /// <exception cref="FormatException">Format exception.</exception>
        public static DateTime CheckDateTime(string dateString)
        {
            var isValidDateTime = DateTime.TryParse(dateString, out var dateTime);
            if (!isValidDateTime)
            {
                throw new FormatException("Format for date was invalid. Must be YYYY-MM-DD.");
            }

            return dateTime;
        }
    }
}