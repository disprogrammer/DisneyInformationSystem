using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using DisneyInformationSystem.ConsoleUI.Inserters;
using DisneyInformationSystem.ConsoleUI.Updaters;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Theme park service class.
    /// </summary>
    public class ThemeParkService
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> class.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> class.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeParkService"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ThemeParkService(IConsole console)
        {
            _console = console;
            _databaseReaderGateway = new DatabaseReaderGateway();
            _databaseWriterGateway = new DatabaseWriterGateway();
        }

        /// <summary>
        /// Gives the user the option to add, update, or delete a theme park.
        /// </summary>
        /// <param name="resort">Resort.</param>
        public void Options(Resort resort)
        {
            var finished = false;
            while (!finished)
            {
                _console.Clear();
                _console.ForegroundColor(DisColors.Cyan);
                _console.WriteLine("===== Theme Park Service =====");

                _console.ForegroundColor(DisColors.Yellow);
                _console.WriteLine("Select an option below that you would like to do.");

                _console.ForegroundColor(DisColors.White);
                var decision = _console.Prompt("1. Add\n" +
                    "2. Update\n" +
                    "3. Delete\n" +
                    "> ");

                switch (decision)
                {
                    case "1":
                        var themeParkInserter = new ThemeParkInserter(_console, _databaseReaderGateway, _databaseWriterGateway, resort.PIN);
                        themeParkInserter.Add();
                        break;

                    case "2":
                        UpdateThemePark(resort);
                        break;

                    case "3":
                        var themeParkToDelete = RetrieveThemePark(resort);
                        var deleter = new DeleterBase(_console, _databaseReaderGateway, _databaseWriterGateway);
                        deleter.DeleteThemePark(themeParkToDelete);
                        break;

                    case "":
                        finished = true;
                        break;

                    default:
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("This is not a valid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Updates theme park properties.
        /// </summary>
        /// <param name="resort">Resort.</param>
        private void UpdateThemePark(Resort resort)
        {
            var themeParkToUpdate = RetrieveThemePark(resort);
            if (themeParkToUpdate != null)
            {
                var updater = new Updater(_console, themeParkToUpdate, _databaseWriterGateway);
                updater.Update();
            }
            else
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("A valid theme park was not selected. Please try again.");
            }
        }

        /// <summary>
        /// Retrieves a theme park in a resort.
        /// </summary>
        /// <param name="resort">Resort.</param>
        /// <returns>Theme park.</returns>
        private ThemePark RetrieveThemePark(Resort resort)
        {
            var themeParks = _databaseReaderGateway.RetrieveListOfThemeParks().Where(park => park.ResortID == resort.PIN);
            _console.ForegroundColor(DisColors.Yellow);
            _console.WriteLine("\nSelect a theme park below to update.");
            _console.ForegroundColor(DisColors.White);
            foreach (var themePark in themeParks)
            {
                _console.WriteLine($"- {themePark.ParkName}");
            }

            var parkDecision = _console.Prompt(">> ").ToLower();
            return themeParks.FirstOrDefault(themePark => themePark.ParkName.ToLower().Contains(parkDecision));
        }
    }
}