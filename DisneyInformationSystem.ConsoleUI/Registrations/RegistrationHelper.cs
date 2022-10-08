using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI.Registrations
{
    /// <summary>
    /// Helper for registering admins and users.
    /// </summary>
    public class RegistrationHelper
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationHelper"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public RegistrationHelper(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Checks if the person did not finish registering or if an exception was thrown.
        /// </summary>
        /// <param name="didNotFinishRegistering">Person did not finish registering.</param>
        /// <param name="exceptionIsThrown">Exception is thrown.</param>
        public void CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(bool didNotFinishRegistering, bool exceptionIsThrown)
        {
            if (didNotFinishRegistering)
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("You did not finish your registration. All information will be lost.");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("Returning to main menu...");
                Thread.Sleep(2000);
            }
            else if (exceptionIsThrown)
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("An error has occurred. All information will be lost.");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("Returning to main menu...");
                Thread.Sleep(2000);
            }
            else
            {
                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine("You are now successfully registered! Thanks for joining! You will be signed in automatically.");
            }
        }
    }
}