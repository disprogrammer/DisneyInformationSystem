using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.MSTests
{
    /// <summary>
    /// Configuration Testing Helper class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class ConfigurationTestingHelper
    {
        /// <summary>
        /// Gets the configuration file for testing projects.
        /// </summary>
        /// <returns>Connection string.</returns>
        public static string GetTestingConfigurationFile()
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