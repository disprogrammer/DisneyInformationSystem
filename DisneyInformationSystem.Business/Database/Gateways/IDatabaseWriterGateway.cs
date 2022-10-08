using DisneyInformationSystem.Business.Database.Records;

namespace DisneyInformationSystem.Business.Database.Gateways
{
    /// <summary>
    /// Database writer gateway interface.
    /// </summary>
    public interface IDatabaseWriterGateway
    {
        /// <summary>
        /// Deletes an admin from the Admins database table.
        /// </summary>
        /// <param name="admin">Admin record.</param>
        void DeleteAdmin(Admin admin);

        /// <summary>
        /// Inserts a new admin into the Admins database table.
        /// </summary>
        /// <param name="admin">Admin record.</param>
        void InsertNewAdmin(Admin admin);

        /// <summary>
        /// Updates an admin in the Admins database table.
        /// </summary>
        /// <param name="admin">Admin record.</param>
        void UpdateAdmin(Admin admin);

        /// <summary>
        /// Deletes an user from the Users database table.
        /// </summary>
        /// <param name="user">User record.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Inserts a new user into the Users database table.
        /// </summary>
        /// <param name="user">User record.</param>
        void InsertNewUser(User user);

        /// <summary>
        /// Updates an user in the Users database table.
        /// </summary>
        /// <param name="user">User record.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Inserts a new resort into the Resorts database table.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        void InsertNewResort(Resort resort);

        /// <summary>
        /// Updates a resort in the Resorts database table.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        void UpdateResort(Resort resort);

        /// <summary>
        /// Inserts a new theme park into the ThemeParks database table.
        /// </summary>
        /// <param name="themePark"></param>
        void InsertNewThemePark(ThemePark themePark);

        /// <summary>
        /// Updates a theme park in the ThemeParks database table.
        /// </summary>
        /// <param name="themePark">Theme park.</param>
        void UpdateThemePark(ThemePark themePark);
    }
}