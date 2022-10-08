using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Sports Domain Facade for Disney Information System console application.
    /// </summary>
    public class SportsDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="SportsDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public SportsDomainFacade(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Allows user to view sports and teams partnered with ESPN.
        /// </summary>
        public void Core()
        {
            _console.ForegroundColor(DisColors.Blue);
            _console.WriteLine("*** Sports is currently under construction. ***");
        }
    }
}