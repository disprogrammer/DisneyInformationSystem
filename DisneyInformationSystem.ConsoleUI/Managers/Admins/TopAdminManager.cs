using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using DisneyInformationSystem.ConsoleUI.Services;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Managers.Admins
{
    /// <summary>
    /// Top Admin Manager class.
    /// </summary>
    public class TopAdminManager : IAdminManager
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of <see cref="AdminManagerHelper"/>.
        /// </summary>
        private readonly AdminManagerHelper _adminManagerHelper;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Initializes a new instance of <see cref="TopAdminManager"/>.
        /// </summary>
        /// <param name="console">Console interface object.</param>
        /// <param name="databaseReaderGateway">Database reader gateway.</param>
        public TopAdminManager(IConsole console, IDatabaseReaderGateway databaseReaderGateway)
        {
            _console = console;
            _adminManagerHelper = new AdminManagerHelper(_console);
            _databaseReaderGateway = databaseReaderGateway;
        }

        /// <inheritdoc />
        public void UpdateCore()
        {
            _console.Clear();
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("===== Top Admin Home =====");

            var exceptionIsThrown = false;
            var finished = false;
            while (!finished)
            {
                try
                {
                    InstructionsAndMenuOptions();

                    var decision = _console.Prompt("> ").ToLower();
                    switch (decision)
                    {
                        case "1":
                            DirectToResortsService();
                            break;

                        case "2":
                            break;

                        case "3":
                            break;

                        case "4":
                            break;

                        case "5":
                            break;

                        case "6":
                            break;

                        case "7":
                            break;

                        case "8":
                            break;

                        case "9":
                            DeleteInformation();
                            break;

                        case "":
                        case "exit":
                            finished = true;
                            break;

                        default:
                            throw new InvalidConsoleTopAdminMainMenuStringException($"'{decision}' is not a valid option. The only valid options are 1-9.");
                    }
                }
                catch (InvalidConsoleTopAdminMainMenuStringException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    _adminManagerHelper.CheckThatAdminIsFinishedOrExceptionIsThrown(exceptionIsThrown, finished);
                }
            }

            _console.Clear();
        }

        /// <summary>
        /// Prints the admins instructions and menu options.
        /// </summary>
        private void InstructionsAndMenuOptions()
        {
            _console.ForegroundColor(DisColors.Yellow);
            _ = _console.TypeString("As a top admin, you are able to add, delete, and update all information in the system.\n");
            _ = _console.TypeString("To begin, please select what you would like to make changes to below.\n");

            _console.ForegroundColor(DisColors.DarkGray);
            _console.WriteLine("Please only type the number that you would like to do.");
            _console.WriteLine("Press [enter] or type 'exit' to return to the previous menu.");

            _console.ForegroundColor(DisColors.White);
            _console.WriteLine("1. Add Resort or Update Resort Information.\n" +
                "2. Add or Update other destinations.\n" +
                "3. Add or Update movies and/or shows.\n" +
                "4. Add or Update assessments.\n" +
                "5. Add or Update sports.\n" +
                "6. Add or Update admins.\n" +
                "7. Add or Update users.\n" +
                "8. Add or Update music.\n" +
                "9. Delete information.");
        }

        /// <summary>
        /// Retrieves admin decision to add or update and directs admin to resorts service.
        /// </summary>
        private void DirectToResortsService()
        {
            var decision = _console.Prompt("Add or Update: ").ToLower();
            if (decision == "add")
            {
                var resortsService = new ResortsService(_console, null, _databaseReaderGateway, new DatabaseWriterGateway());
                resortsService.Add();
            }
            else if (decision == "update")
            {
                var listOfResorts = _databaseReaderGateway.RetrieveListOfResorts();
                _console.WriteLine("Resorts: ");
                foreach (var resort in listOfResorts)
                {
                    _console.WriteLine($"- {resort.ResortName}");
                }

                var resortDecision = _console.Prompt("Resort: ").ToLower();
                if (listOfResorts.Any(resort => resort.ResortName.ToLower().Contains(resortDecision)))
                {
                    var resort = listOfResorts.First(resort => resort.ResortName.ToLower().Contains(resortDecision));
                    var resortsService = new ResortsService(_console, resort, _databaseReaderGateway, new DatabaseWriterGateway());
                    resortsService.Update();
                }
            }
            else 
            {
                _console.WriteLine("No selection was made to add or update a resort. You will be returned to your admin menu.");
            }
        }

        /// <summary>
        /// Allows the admin to delete information from the database.
        /// </summary>
        private void DeleteInformation()
        {
            _console.WriteLine("Select one of the options below to begin.");
            _console.WriteLine("1. Resorts\n" +
                "2. Movies and TV\n" +
                "3. Users\n" +
                "4. Admins");
            var decision = _console.Prompt("> ");

            switch (decision)
            {
                case "1":
                    _console.WriteLine("Provide the name of the resort you would like to make changes to.");
                    var resortName = _console.Prompt("Resort name: ").ToLower();
                    var selectedResort = _databaseReaderGateway.RetrieveListOfResorts().First(resort => resort.ResortName.ToLower().Contains(resortName));
                    var resortsService = new ResortsService(_console, selectedResort, _databaseReaderGateway, new DatabaseWriterGateway());
                    resortsService.Delete();
                    break;

                case "":
                    break;

                default:
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine("This is not a valid decision. Please try again.");
                    break;
            }
        }
    }
}