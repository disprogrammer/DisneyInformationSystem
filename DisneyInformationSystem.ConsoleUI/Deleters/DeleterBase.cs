using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Deleters
{
    /// <summary>
    /// Deleter class.
    /// </summary>
    public class DeleterBase : IDeleterBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleterBase"/> class.
        /// </summary>
        /// <param name="console"></param>
        /// <param name="databaseReaderGateway"></param>
        /// <param name="databaseWriterGateway"></param>
        public DeleterBase(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void DeleteEntertainmentVenues(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void DeleteResortHotels(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void DeleteThemeParks(string resortPin)
        {
            var listOfThemeParks = _databaseReaderGateway.RetrieveListOfThemeParks().Where(themePark => themePark.ResortID == resortPin).ToList();
            foreach (var themePark in listOfThemeParks)
            {
                var propertyToUpdate = themePark.GetType().GetProperty("Operating");
                propertyToUpdate.SetValue(themePark, false, null);
                _databaseWriterGateway.Update(themePark);

                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine($"Theme Park has successfully been updated. The operating value is now {propertyToUpdate.GetValue(themePark, null)}.");
            }
        }

        /// <inheritdoc />
        public void DeleteTransportation(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void DeleteWaterParks(string resortPin)
        {
            // Method intentionally left empty.
        }
    }
}