using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Inserters;

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
        /// Initializes a new instance of the <see cref="ThemeParkService"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ThemeParkService(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Gives the user the option to add, update, or delete a theme park.
        /// </summary>
        /// <param name="resortAcronym">Resort acronym.</param>
        public void Options(string resortAcronym)
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
                        var themeParkInserter = new ThemeParkInserter(_console, new DatabaseReaderGateway(), new DatabaseWriterGateway(), resortAcronym);
                        themeParkInserter.Add();
                        break;

                    case "2":
                        break;

                    case "3":
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
    }
}