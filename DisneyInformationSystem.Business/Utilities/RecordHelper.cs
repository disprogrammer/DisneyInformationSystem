using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Record helper class.
    /// </summary>
    /// <typeparam name="T">Generic record.</typeparam>
    public static class RecordHelper<T> where T : GenericRecord
    {
        /// <summary>
        /// Checks if the acronym is already in use.
        /// </summary>
        /// <param name="listOfRecords">List of records.</param>
        /// <param name="acronym">Acronym.</param>
        /// <returns>True if acronym is already in use; false otherwise.</returns>
        public static bool AcronymIsAlreadyInUse(List<T> listOfRecords, string acronym)
        {
            var listOfPins = listOfRecords.Select(record => record.PIN);
            if (listOfPins.Contains(acronym))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Signs in the admin.
        /// </summary>
        /// <param name="emailAddress">Email Address.</param>
        /// <param name="password">Password.</param>
        /// <returns>Admin.</returns>
        public static Tuple<Admin, bool> AdminSignIn(Admin admin, string emailAddress, string password)
        {
            RetrievePerson(emailAddress, password, admin);
            return new Tuple<Admin, bool>(admin, true);
        }

        /// <summary>
        /// Signs in the user.
        /// </summary>
        /// <param name="emailAddress">Email Address.</param>
        /// <param name="password">Password.</param>
        /// <returns>User.</returns>
        public static Tuple<User, bool> UserSignIn(User user, string emailAddress, string password)
        {
            RetrievePerson(emailAddress, password, user);
            return new Tuple<User, bool>(user, true);
        }

        /// <summary>
        /// Retrieves a list of properties and their values.
        /// </summary>
        /// <param name="record">Record.</param>
        /// <returns>List of strings.</returns>
        public static List<string> RetrieveListOfPropertiesAndValues(T record)
        {
            var listOfPropertiesAndValues = new List<string>();
            var recordType = record.GetType();
            var properties = recordType.GetProperties().Where(prop => prop.Name != "PIN" && !prop.Name.Contains("ID"));

            foreach (var property in properties)
            {
                var propertyName = StringHelper.SplitObjectsAndPropertiesWords(property.Name);
                listOfPropertiesAndValues.Add($"{propertyName}: {property.GetValue(record)}");
            }

            return listOfPropertiesAndValues;
        }

        /// <summary>
        /// Retrieves the person logging in.
        /// </summary>
        /// <param name="emailAddress">Email address.</param>
        /// <param name="password">Password.</param>
        /// <param name="person">Person record.</param>
        /// <exception cref="EmailNotFoundException">Email not found exception.</exception>
        /// <exception cref="InvalidPasswordException">Invalid password exception.</exception>
        private static void RetrievePerson(string emailAddress, string password, Person person)
        {
            if (person == null)
            {
                throw new EmailNotFoundException($"{emailAddress} was not found in our database. Please try again.");
            }

            var passwordsMatch = SecurePasswordHasher.Verify(password, person.Password);
            if (!passwordsMatch)
            {
                throw new InvalidPasswordException($"{password} is invalid and does not match our records.");
            }
        }
    }
}