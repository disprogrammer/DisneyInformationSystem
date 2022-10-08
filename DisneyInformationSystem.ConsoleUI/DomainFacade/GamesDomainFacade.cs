using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Games Domain Facade for Disney Information System console application.
    /// </summary>
    public class GamesDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public GamesDomainFacade(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Allows user to view and play games.
        /// </summary>
        public void Core()
        {
            _console.ForegroundColor(DisColors.Blue);
            _console.WriteLine("*** Games is currently under construction. ***");
        }
    }
}