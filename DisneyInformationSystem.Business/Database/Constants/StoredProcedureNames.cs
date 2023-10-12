using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.Database.Constants
{
    /// <summary>
    /// Constant string names of stored procedures in the database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class StoredProcedureNames
    {
        public const string AdminByEmail = "AdminByEmail";
        public const string AdminByPin = "AdminByPin";
        public const string AllAdmins = "AllAdmins";
        public const string AllAdminTypes = "AllAdminTypes";
        public const string DeleteAdmin = "DeleteAdmin";
        public const string InsertNewAdmin = "InsertNewAdmin";
        public const string UpdateAdmin = "UpdateAdmin";

        public const string AllUsers = "AllUsers";
        public const string DeleteUser = "DeleteUser";
        public const string InsertNewUser = "InsertNewUser";
        public const string UpdateUser = "UpdateUser";
        public const string UserByEmail = "UserByEmail";
        public const string UserByPin = "UserByPin";

        public const string AllResortHotels = "AllResortHotels";
        public const string InsertNewResortHotel = "InsertNewResortHotel";
        public const string ResortHotelByName = "ResortHotelByName";
        public const string ResortHotelsByResortID = "ResortHotelsByResortID";
        public const string UpdateResortHotel = "UpdateResortHotel";

        public const string AllResorts = "AllResorts";
        public const string InsertNewResort = "InsertNewResort";
        public const string ResortByName = "ResortByName";
        public const string ResortByPin = "ResortByPin";
        public const string UpdateResort = "UpdateResort";

        public const string AllThemeParks = "AllThemeParks";
        public const string InsertNewThemePark = "InsertNewThemePark";
        public const string ThemeParkByName = "ThemeParkByName";
        public const string ThemeParksByResortID = "ThemeParksByResortID";
        public const string UpdateThemePark = "UpdateThemePark";

        /// <summary>
        /// List of delete stored procedures.
        /// </summary>
        public static List<string> DeleteStoredProcedures => new()
        {
            DeleteAdmin,
            DeleteUser
        };

        /// <summary>
        /// List of insert stored procedures.
        /// </summary>
        public static List<string> InsertStoredProcedures => new()
        {
            InsertNewAdmin,
            InsertNewUser,
            InsertNewResortHotel,
            InsertNewResort,
            InsertNewThemePark
        };

        /// <summary>
        /// List of update stored procedures.
        /// </summary>
        public static List<string> UpdateStoredProcedures => new()
        {
            UpdateAdmin,
            UpdateUser,
            UpdateResortHotel,
            UpdateResort,
            UpdateThemePark
        };
    }
}