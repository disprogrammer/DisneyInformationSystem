using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Book Domain Facade for Disney Information System console application.
    /// </summary>
    public class BookDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public BookDomainFacade(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Allows user to select where they would like to book a vacation.
        /// </summary>
        public void Core()
        {
            _console.ForegroundColor(DisColors.Blue);
            _console.WriteLine("*** Book is currently under construction. ***");
        }
    }
}