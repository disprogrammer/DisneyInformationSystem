using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.Assessments;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.DomainFacade;
using DisneyInformationSystem.ConsoleUI.Helpers;
using DisneyInformationSystem.ConsoleUI.Registrations;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI
{
    /// <summary>
    /// The Console User Interface for the Dis Application.
    /// </summary>
    public class DisneyInformationSystemConsoleUi
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="IDatabaseReaderGateway"/> interface.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="IDatabaseWriterGateway"/> interface.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Gets or sets the person signed in.
        /// </summary>
        private Person PersonSignedIn { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisneyInformationSystemConsoleUi"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        public DisneyInformationSystemConsoleUi(IConsole console)
        {
            _console = console;
            _databaseReaderGateway = new DatabaseReaderGateway();
            _databaseWriterGateway = new DatabaseWriterGateway();
        }

        /// <summary>
        /// Main method that starts the Dis Console Application. Called in the main method in Program.cs.
        /// </summary>
        public void Run()
        {
            var finished = false;
            var exceptionIsThrown = false;

            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("========== Welcome to the DIS Application! ==========");

            _console.ForegroundColor(DisColors.DarkGray);
            _console.WriteLine("Your home to all things Disney!");
            _console.WriteLine("Press [enter] at any menu or input to go back to the previous option(s).");
            _console.WriteLine(string.Empty);

            while (!finished)
            {
                _console.Title("Disney Information System - Home");

                try
                {
                    _console.ForegroundColor(DisColors.Yellow);
                    _console.TypeString("Make a selection below of what you would like to do!\n");

                    _console.ForegroundColor(DisColors.White);
                    _console.WriteLine("Type 'sign in' to sign into your account or 'register' to create a new account!");
                    _console.WriteLine("Type 'log out' to sign out of your account.");
                    var mainMenuUserDecision = _console.Prompt("Explore, Book, List, Music, Games, Sports, Admin, or Exit: ").ToLower();
                    var personString = StringHelper.PersonTitleString(PersonSignedIn);

                    switch (mainMenuUserDecision)
                    {
                        case "explore":
                            _console.Clear();
                            _console.Title($"Disney Information System - Explore Home; {personString}");
                            var exploreDomainFacade = new ExploreDomainFacade(_console, personString);
                            exploreDomainFacade.Core();
                            break;

                        case "book":
                            _console.Clear();
                            _console.Title($"Disney Information System - Book Home; {personString}");
                            var bookDomainFacade = new BookDomainFacade(_console);
                            bookDomainFacade.Core();
                            break;

                        case "list":
                            _console.Clear();
                            _console.Title($"Disney Information System - List Home; {personString}");
                            var listDomainFacade = new ListDomainFacade(_console);
                            listDomainFacade.Core();
                            break;

                        case "music":
                            _console.Clear();
                            _console.Title($"Disney Information System - Music Home; {personString}");
                            var musicDomainFacade = new MusicDomainFacade(_console);
                            musicDomainFacade.Core();
                            break;

                        case "games":
                            _console.Clear();
                            _console.Title($"Disney Information System - Games Home; {personString}");
                            var gamesDomainFacade = new GamesDomainFacade(_console);
                            gamesDomainFacade.Core();
                            break;

                        case "sports":
                            _console.Clear();
                            _console.Title($"Disney Information System - Sports Home; {personString}");
                            var sportsDomainFacade = new SportsDomainFacade(_console);
                            sportsDomainFacade.Core();
                            break;

                        case "admin":
                            if (PersonSignedIn == null)
                            {
                                _console.ForegroundColor(DisColors.Red);
                                _console.WriteLine("There is no admin signed in. You can not continue any further.");
                                _console.WriteLine("Returning to the main menu...");
                                break;
                            }

                            if (PersonSignedIn.PIN.StartsWith('A'))
                            {
                                _console.Clear();
                                _console.Title($"Disney Information System - Admin Home; {personString}");
                                var personSignedIn = (Admin)PersonSignedIn;
                                var adminDomainFacade = new AdminDomainFacade(_console, personSignedIn.AdminTypeCode, _databaseReaderGateway);
                                adminDomainFacade.Core();
                            }
                            break;

                        case "sign in":
                            _console.Clear();
                            SignIn();
                            break;

                        case "register":
                            _console.Clear();
                            Register();
                            break;

                        case "log out":
                            _console.Clear();
                            Logout();
                            break;

                        case "":
                        case "exit":
                            finished = true;
                            break;

                        default:
                            throw new InvalidConsoleMainMenuStringException($"'{mainMenuUserDecision}' is not a valid Main Menu option. Must choose a valid option.");
                    }
                }
                catch (InvalidConsoleMainMenuStringException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                catch (InvalidRegisterTypeException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    CheckIfFinishedOrExceptionIsThrown(finished, exceptionIsThrown);
                }
            }
        }

        /// <summary>
        /// Allows person to sign in.
        /// </summary>
        private void SignIn()
        {
            if (PersonSignedIn != null)
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("A person is already signed in. Please log out before signing in a different person.");
            }
            else
            {
                var signInHelper = new SignInHelper(_console, _databaseReaderGateway);
                PersonSignedIn = signInHelper.SignIn();
            }

            _console.Clear();
        }

        /// <summary>
        /// Allows a person to register as an admin or user.
        /// </summary>
        private void Register()
        {
            var userAdmin = _console.Prompt("Admin or User: ").ToLower();

            switch (userAdmin)
            {
                case "user":
                    _console.Title("Disney Information System - User Registration");
                    var userRegistration = new UserRegistration(_console, _databaseReaderGateway, _databaseWriterGateway);
                    PersonSignedIn = userRegistration.Register();
                    break;

                case "admin":
                    _console.Title("Disney Information System - Admin Registration");
                    var assessmentManager = new AssessmentManager(_console, "./Assessments/");
                    var adminRegistration = new AdminRegistration(_console, _databaseReaderGateway, _databaseWriterGateway, assessmentManager);
                    PersonSignedIn = adminRegistration.Register();
                    break;

                case "":
                case "exit":
                    PersonSignedIn = null;
                    break;

                default:
                    throw new InvalidRegisterTypeException($"{userAdmin} is not valid. Must be Admin or User.");
            }

            _console.Clear();
        }

        /// <summary>
        /// Prints message that a person is being logged out.
        /// </summary>
        private void Logout()
        {
            if (PersonSignedIn == null)
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("No one is currently signed in to be able to log out.");
            }
            else
            {
                if (PersonSignedIn.PIN.StartsWith('A'))
                {
                    Admin admin = (Admin)PersonSignedIn;
                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine($"{admin.FirstName} {admin.LastName} is being signed out.");
                    PersonSignedIn = null;
                }
                else
                {
                    User user = (User)PersonSignedIn;
                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine($"{user.FirstName} {user.LastName} is being signed out.");
                    PersonSignedIn = null;
                }
            }

            _console.Clear();
        }

        /// <summary>
        /// Checks if finished is true or if an exception was thrown.
        /// </summary>
        /// <param name="finished">Finished boolean.</param>
        /// <param name="exceptionIsThrown">Exception is thrown boolean.</param>
        private void CheckIfFinishedOrExceptionIsThrown(bool finished, bool exceptionIsThrown)
        {
            if (exceptionIsThrown)
            {
                _console.WriteLine("Please try again.");
            }

            if (finished)
            {
                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine("Thanks for exploring all things Disney! Come back real soon!");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("This application will close in three seconds...");
                Thread.Sleep(3000);
            }
        }
    }
}