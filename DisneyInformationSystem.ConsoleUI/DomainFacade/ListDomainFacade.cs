using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// List Domain Facade for Disney Information System console application.
    /// </summary>
    public class ListDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public ListDomainFacade(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Allows user to view vacation information as well as profile information.
        /// </summary>
        public void Core()
        {
            _console.ForegroundColor(DisColors.Blue);
            _console.WriteLine("*** List is currently under construction. ***");
        }
    }
}