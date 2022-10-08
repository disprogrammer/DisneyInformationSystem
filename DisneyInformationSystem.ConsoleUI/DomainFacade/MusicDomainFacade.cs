using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Music Domain Facade for Disney Information System console application.
    /// </summary>
    public class MusicDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public MusicDomainFacade(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Allows user to view music options, play music, and create a play-list.
        /// </summary>
        public void Core()
        {
            _console.ForegroundColor(DisColors.Blue);
            _console.WriteLine("*** Music is currently under construction. ***");
        }
    }
}