using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.Managers
{
    /// <summary>
    /// Destinations Manager for the Disney Information System console application.
    /// </summary>
    public class DestinationsManager
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="DestinationsManager"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public DestinationsManager(IConsole console)
        {
            _console = console;
        }

        public void Explore()
        {
            _console.WriteLine("Under construction.");
        }
    }
}