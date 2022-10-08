using System.Configuration;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// ConfigurationHelper class.
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Gets the connection string from the configuration file.
        /// </summary>
        /// <returns>Connection string.</returns>
        public static string ConnectionString()
        {
            ExeConfigurationFileMap configFileMap = new()
            {
                ExeConfigFilename = "App.config"
            };

            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            var section = (ConnectionStringsSection)config.GetSection("connectionStrings");
            var connectionStringCollection = section.ConnectionStrings;

            var connectionString = string.Empty;
            foreach (var connection in connectionStringCollection)
            {
                if (!connection.ToString().Contains("DESKTOP"))
                {
                    continue;
                }

                connectionString = connection.ToString();
            }

            return connectionString;
        }
    }
}