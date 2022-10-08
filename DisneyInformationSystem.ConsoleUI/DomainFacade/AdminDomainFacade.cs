using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers.Admins;
using System.Collections.Generic;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.DomainFacade
{
    /// <summary>
    /// Admin Domain Facade for Disney Information System console application.
    /// </summary>
    public class AdminDomainFacade : IDomainFacadeBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Admin Type Code of the current admin signed in.
        /// </summary>
        private readonly string _adminTypeCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="IDatabaseReaderGateway"/> interface.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// List of resort admin types.
        /// </summary>
        private readonly List<string> ListOfResortAdminTypes = new() { "DLR", "DPA", "HKR", "SDR", "TDR", "WDW" };

        /// <summary>
        /// List of entertainment and destinations admin types.
        /// </summary>
        private readonly List<string> ListOfDestinationAndEntertainmentAdminTypes = new() { "ABD", "DCL", "DSA", "MAD", "NGE", "TVA", "UAD" };

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminDomainFacade"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        /// <param name="adminTypeCode">The admin type code of the admin that is currently signed in.</param>
        public AdminDomainFacade(IConsole console, string adminTypeCode, IDatabaseReaderGateway databaseReaderGateway)
        {
            _console = console;
            _adminTypeCode = adminTypeCode;
            _databaseReaderGateway = databaseReaderGateway;
        }

        /// <summary>
        /// Allows admins to sign in or register to make changes to Disney information.
        /// </summary>
        public void Core()
        {
            var finished = false;

            while (!finished)
            {
                var databaseHasResorts = _databaseReaderGateway.RetrieveListOfResorts().Any();
                if (_adminTypeCode == "TAD")
                {
                    var topAdminManager = new TopAdminManager(_console, _databaseReaderGateway);
                    topAdminManager.UpdateCore();
                }
                else if (ListOfResortAdminTypes.Contains(_adminTypeCode) && databaseHasResorts)
                {
                    var resortsAdminManager = new ResortsAdminManager(_console, _adminTypeCode, _databaseReaderGateway);
                    resortsAdminManager.UpdateCore();
                }
                else if (ListOfDestinationAndEntertainmentAdminTypes.Contains(_adminTypeCode))
                {
                    var destinationsAndEntertainmentAdminManager = new DestinationsAndEntertainmentAdminManager(_console);
                    destinationsAndEntertainmentAdminManager.UpdateCore();
                }

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("Are you finished? (Y/N)");
                var adminDecision = _console.Prompt("> ").ToLower();

                if (adminDecision == "y" || adminDecision == "yes")
                {
                    finished = true;
                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine("Thank you for keeping our system up to date.");
                    _console.WriteLine("Returning to the main menu...");
                }
                else
                {
                    finished = false;
                    _console.WriteLine("Returning you to your admin menu...");
                    _console.Clear();
                }
            }
        }
    }
}