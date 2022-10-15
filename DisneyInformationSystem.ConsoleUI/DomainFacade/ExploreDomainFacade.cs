using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Explore Domain Facade for Disney Information System console application.
    /// </summary>
    public class ExploreDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// The person logged in with their ID, first name, and last name.
        /// </summary>
        private readonly string _personString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExploreDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        /// <param name="personString">Person string.</param>
        public ExploreDomainFacade(IConsole console, string personString)
        {
            _console = console;
            _personString = personString;
        }

        /// <summary>
        /// Allows user to select where they would like to explore.
        /// </summary>
        public void Core()
        {
            var doneExploring = false;
            var exceptionIsThrown = false;

            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("===== Explore =====");

            _console.ForegroundColor(DisColors.DarkGray);
            _console.WriteLine("Explore from the many destinations to the vast entertainment!");
            _console.WriteLine("Press [enter] or type 'exit' to return to the main menu.");

            while (!doneExploring)
            {
                try
                {
                    _console.ForegroundColor(DisColors.Yellow);
                    _console.WriteLine(string.Empty);
                    _console.TypeString("Make a selection below of what you would like to do!\n");

                    _console.ForegroundColor(DisColors.White);
                    var exploreDecision = _console.Prompt("1. Destinations\n" +
                                                          "2. Entertainment\n" +
                                                          "3. Exit\n" +
                                                          "> ").ToLower();

                    switch (exploreDecision)
                    {
                        case "1":
                        case "destinations":
                            _console.Title($"Disney Information System - Destinations; {_personString}");
                            var destinationsManager = new DestinationsManager(_console);
                            destinationsManager.Explore();
                            break;

                        case "2":
                        case "entertainment":
                            _console.Title($"Disney Information System - Entertainment; {_personString}");
                            break;

                        case "":
                        case "3":
                        case "exit":
                            doneExploring = true;
                            break;

                        default:
                            throw new InvalidConsoleExploreMenuStringException($"'{exploreDecision}' is not a valid menu option. Must choose a valid option.");
                    }
                }
                catch (InvalidConsoleExploreMenuStringException exception)
                {
                    var exceptionType = StringHelper.SplitObjectsAndPropertiesWords(exception.GetType().Name);
                    exceptionIsThrown = true;

                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine($"Exception Type: {exceptionType}\n" +
                                       $"Exception Message: {exception.Message}\n" +
                                       $"Stack Trace: {exception.StackTrace}");
                }
                finally
                {
                    CheckIfFinishedOrExceptionIsThrown(doneExploring, exceptionIsThrown);
                }
            }

            _console.Clear();
        }

        /// <summary>
        /// Checks if done exploring is true or if an exception was thrown.
        /// </summary>
        /// <param name="doneExploring">Done exploring boolean.</param>
        /// <param name="exceptionIsThrown">Exception is thrown boolean.</param>
        private void CheckIfFinishedOrExceptionIsThrown(bool doneExploring, bool exceptionIsThrown)
        {
            if (exceptionIsThrown)
            {
                _console.WriteLine("Please try again.");
            }

            if (doneExploring)
            {
                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine("We hope you had fun exploring around the Disney Company!");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("You are now returning to the main menu...");
                Thread.Sleep(2000);
            }
        }
    }
}