using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.Managers.Admins
{
    /// <summary>
    /// Destinations and entertainment admin manager class.
    /// </summary>
    public class DestinationsAndEntertainmentAdminManager : IAdminManager
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of <see cref="DestinationsAndEntertainmentAdminManager"/>.
        /// </summary>
        /// <param name="console">Console interface object.</param>
        public DestinationsAndEntertainmentAdminManager(IConsole console)
        {
            _console = console;
        }

        /// <inheritdoc />
        public void UpdateCore()
        {
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("===== Destinations and Entertainment Admin Home =====");

            var finished = false;
            while (!finished)
            {
                _console.ForegroundColor(DisColors.Yellow);
                _console.TypeString("As a destinations/entertainment admin, you are able to add, delete, and update all information in the area where you are an admin.\n");
                _console.TypeString("To begin, please select what you would like to make changes to below.\n");

                _console.ForegroundColor(DisColors.DarkGray);
                _console.WriteLine("Press [enter] or type 'exit' to return to the previous menu.");

                _console.ForegroundColor(DisColors.White);

                var decision = _console.Prompt("> ").ToLower();
                switch (decision)
                {
                    case "":
                    case "exit":
                        finished = true;
                        _console.ForegroundColor(DisColors.Green);
                        _console.WriteLine("Thank you for your contributions to the Disney Information System.");
                        break;
                }
            }
        }
    }
}