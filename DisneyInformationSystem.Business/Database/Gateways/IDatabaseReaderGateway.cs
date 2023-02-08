using DisneyInformationSystem.Business.Database.Records;
using System.Collections.Generic;

namespace DisneyInformationSystem.Business.Database.Gateways
{
    /// <summary>
    /// Database reader gateway interface.
    /// </summary>
    public interface IDatabaseReaderGateway
    {
        /// <summary>
        /// Retrieves admin from database by email address.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>Admin.</returns>
        Admin RetrieveAdminByEmail(string email);

        /// <summary>
        /// Retrieves admin from database by pin.
        /// </summary>
        /// <param name="id">Admin pin.</param>
        /// <returns>Admin.</returns>
        Admin RetrieveAdminById(string id);

        /// <summary>
        /// Retrieves a list of admins from the database.
        /// </summary>
        /// <returns>List of admins.</returns>
        List<Admin> RetrieveListOfAdmins();

        /// <summary>
        /// Retrieves a list of admin types from the database.
        /// </summary>
        /// <returns>List of admin types.</returns>
        List<AdminTypes> RetrieveListOfAdminTypes();

        /// <summary>
        /// Retrieves a list of users from the database.
        /// </summary>
        /// <returns>List of users.</returns>
        List<User> RetrieveListOfUsers();

        /// <summary>
        /// Retrieves user from database by email address.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>User.</returns>
        User RetrieveUserByEmail(string email);

        /// <summary>
        /// Retrieves user from database by pin.
        /// </summary>
        /// <param name="id">Admin pin.</param>
        /// <returns>User.</returns>
        User RetrieveUserById(string id);

        /// <summary>
        /// Retrieves a list of resorts from the database.
        /// </summary>
        /// <returns>List of resorts.</returns>
        List<Resort> RetrieveListOfResorts();

        /// <summary>
        /// Retrieves a resort by name from the database.
        /// </summary>
        /// <param name="input">Input to filter.</param>
        /// <returns>Resort.</returns>
        Resort RetrieveResortByName(string input);

        /// <summary>
        /// Retrieves a resort by pin from the database.
        /// </summary>
        /// <param name="id">Pin.</param>
        /// <returns>Resort.</returns>
        Resort RetrieveResortByPin(string id);

        /// <summary>
        /// Retrieves a list of theme parks from the database.
        /// </summary>
        /// <returns>List of theme parks.</returns>
        List<ThemePark> RetrieveListOfThemeParks();

        /// <summary>
        /// Retrieves a list of theme parks from the database by resort id.
        /// </summary>
        /// <param name="resortId">Resort id.</param>
        /// <returns>List of theme parks.</returns>
        List<ThemePark> RetrieveThemeParksByResortID(string resortId);

        /// <summary>
        /// Retrieves a list of resort hotels from the database by resort id.
        /// </summary>
        /// <param name="resortId">Resort id.</param>
        /// <returns>List of resort hotels.</returns>
        List<ResortHotel> RetrieveResortHotelsByResortID(string resortId);
    }
}