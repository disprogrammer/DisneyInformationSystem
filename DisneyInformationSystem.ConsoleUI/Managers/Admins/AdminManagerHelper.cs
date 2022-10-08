using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.Managers.Admins
{
    /// <summary>
    /// Admin manager helper.
    /// </summary>
    public class AdminManagerHelper
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes new instance of <see cref="AdminManagerHelper"/>.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public AdminManagerHelper(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Checks if admin is finished or if an exception was thrown.
        /// </summary>
        /// <param name="exceptionIsThrown">Exception is thrown.</param>
        /// <param name="finished">Finished.</param>
        public void CheckThatAdminIsFinishedOrExceptionIsThrown(bool exceptionIsThrown, bool finished)
        {
            if (finished && !exceptionIsThrown)
            {
                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine("Thank you for you your contributions to the Disney Information System.");
                _console.WriteLine("Returning to the Admin menu...");
            }

            if (exceptionIsThrown && !finished)
            {
                _console.WriteLine("Please try again.");
            }
        }
    }
}