using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using DisneyInformationSystem.ConsoleUI.Helpers;
using DisneyInformationSystem.ConsoleUI.Services.Helpers;
using DisneyInformationSystem.ConsoleUI.Updaters;
using System;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Resorts service class.
    /// </summary>
    public class ResortsService : IServiceBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Resort object.
        /// </summary>
        private Resort _resort;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of <see cref="ResortsService"/>.
        /// </summary>
        /// <param name="console">Console interface.</param>
        /// <param name="resort">Resorts record.</param>
        /// <param name="databaseReaderGateway">Database reader gateway.</param>
        /// <param name="databaseWriterGateway">Database writer gateway.</param>
        public ResortsService(IConsole console, Resort resort, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _resort = resort;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void Add()
        {
            var finished = false;
            var exceptionIsThrown = false;
            var servicesHelper = new ServicesHelper(_console);
            while (!finished)
            {
                try
                {
                    _console.ForegroundColor(DisColors.Cyan);
                    _console.WriteLine("\n===== Adding Resort =====");

                    _console.ForegroundColor(DisColors.Yellow);
                    _console.TypeString("Provide the information below to add a resort to the database.\n");

                    _console.ForegroundColor(DisColors.DarkGray);
                    _console.WriteLine("If you do not provide any information for the data fields, you will lose your inputs.");

                    _console.ForegroundColor(DisColors.White);
                    var resortAcronym = _console.Prompt("Resort Acronym (3 letters): ").ToUpper();
                    if (string.IsNullOrWhiteSpace(resortAcronym))
                    {
                        finished = true;
                        break;
                    }

                    var acronymAlreadyInUse = RecordHelper<Resort>.AcronymIsAlreadyInUse(_databaseReaderGateway.RetrieveListOfResorts(), resortAcronym);
                    if (acronymAlreadyInUse)
                    {
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("The provided acronym is already used for another resort. Please try again.");
                        continue;
                    }

                    ExceptionHandler.CheckIfAcronymIsValid(resortAcronym);

                    var resortName = _console.Prompt("Resort Name: ");
                    if (string.IsNullOrWhiteSpace(resortName))
                    {
                        finished = true;
                        break;
                    }

                    var addressOfResort = _console.Prompt("Address of Resort: ");
                    if (string.IsNullOrWhiteSpace(addressOfResort))
                    {
                        finished = true;
                        break;
                    }

                    var phoneNumber = _console.Prompt("Phone Number: ");
                    if (string.IsNullOrWhiteSpace(phoneNumber))
                    {
                        finished = true;
                        break;
                    }

                    ExceptionHandler.CheckIfPhoneNumberIsValid(phoneNumber);

                    _console.ForegroundColor(DisColors.Yellow);
                    _console.TypeString("For the entries of inputting numbers, 0 is defaulted if nothing is entered.\n");

                    _console.ForegroundColor(DisColors.White);
                    var numberOfThemeParksInteger = servicesHelper.RetrieveNumber("Number of Theme Parks (number only): ");
                    var numberOfResortHotelsInteger = servicesHelper.RetrieveNumber("Number of Resort Hotels (number only): ");
                    var numberOfPartnerHotelsInteger = servicesHelper.RetrieveNumber("Number of Partner Hotels (number only): ");
                    var numberOfWaterParksInteger = servicesHelper.RetrieveNumber("Number of Water Parks (number only): ");
                    var numberOfEntertainmentVenuesInteger = servicesHelper.RetrieveNumber("Number of Entertainment Venues (number only): ");
                    var operatingValue = servicesHelper.RetrieveOperatingValue("resort");
                    var openingDateTime = servicesHelper.RetrieveOpeningDate();
                    var closingDateTime = servicesHelper.RetrieveClosingDate(operatingValue);

                    var resort = new Resort(
                        resortAcronym,
                        resortName,
                        addressOfResort,
                        phoneNumber,
                        numberOfThemeParksInteger,
                        numberOfResortHotelsInteger,
                        numberOfPartnerHotelsInteger,
                        numberOfWaterParksInteger,
                        numberOfEntertainmentVenuesInteger,
                        operatingValue,
                        openingDateTime,
                        closingDateTime);

                    _databaseWriterGateway.Insert(resort);

                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine("The resort was added to the database successfully!");
                }
                catch (DisApplicationTechnicalException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                catch (FormatException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    servicesHelper.CheckIfFinsihedOrExceptionIsThrown(exceptionIsThrown, finished);
                }
            }
        }

        /// <inheritdoc />
        public void Delete()
        {
            _console.ForegroundColor(DisColors.White);
            _console.WriteLine($"The resort, {_resort.ResortName}, will be NOT be removed, but will set the 'Operating' field to FALSE.");
            _console.WriteLine($"The resorts closing date will be set to today's date.");
            _console.WriteLine("This will also set all the parks, resort hotels, attractions, etc. to FALSE as well.");
            Thread.Sleep(2000);

            var matchingResort = _databaseReaderGateway.RetrieveResortByPin(_resort.PIN);
            _console.WriteLine($"Setting resort, {matchingResort.ResortName}, to not operating...");

            _resort = new Resort(
                _resort.PIN,
                _resort.ResortName,
                _resort.AddressOfResort,
                _resort.Phone,
                _resort.NumberOfThemeParks,
                _resort.NumberOfResortHotels,
                _resort.NumberOfPartnerHotels,
                _resort.NumberOfWaterParks,
                _resort.NumberOfEntertainmentVenues,
                false,
                _resort.OpeningDate,
                DateTime.Today);
            _databaseWriterGateway.Update(_resort);

            _console.ForegroundColor(DisColors.Green);
            if (!_resort.Operating)
            {
                _console.WriteLine($"Resort has successfully been updated. The operating value is now FALSE.");
            }

            _console.ForegroundColor(DisColors.White);
            _console.WriteLine($"Setting everything within {_resort.ResortName} as NOT operating...");

            var deleter = new DeleterBase(_console, _databaseReaderGateway, _databaseWriterGateway);
            deleter.DeleteThemeParks(_resort.PIN);
            deleter.DeleteResortHotels(_resort.PIN);
            deleter.DeleteTransportation(_resort.PIN);
            deleter.DeleteEntertainmentVenues(_resort.PIN);
            deleter.DeleteWaterParks(_resort.PIN);
        }

        /// <inheritdoc />
        public void Update()
        {
            _console.ForegroundColor(DisColors.White);
            var updatingResortInformationDecision = _console.Prompt("Updating resort information? (Y/N): ").ToLower();
            if (updatingResortInformationDecision == "y")
            {
                _console.ForegroundColor(DisColors.Blue);
                _console.WriteLine("\n=== Resort Information ===");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine($"Resort Name: {_resort.ResortName}\n" +
                    $"Resort Address: {_resort.AddressOfResort}\n" +
                    $"Phone: {_resort.Phone}\n" +
                    $"Number of Theme Parks: {_resort.NumberOfThemeParks}\n" +
                    $"Number of Resort Hotels: {_resort.NumberOfResortHotels}\n" +
                    $"Number of Partner Hotels: {_resort.NumberOfPartnerHotels}\n" +
                    $"Number of Water Parks: {_resort.NumberOfWaterParks}\n" +
                    $"Number of Entertainment Venues: {_resort.NumberOfEntertainmentVenues}\n" +
                    $"Operating: {_resort.Operating}\n" +
                    $"Opening Date: {_resort.OpeningDate}\n" +
                    $"Closing Date: {_resort.ClosingDate}");

                var updater = new Updater(_console, _resort, _databaseWriterGateway);
                updater.Update();
            }
            else if (updatingResortInformationDecision == "n")
            {
                _console.ForegroundColor(DisColors.Yellow);
                _console.TypeString($"Select what else you would like to update in the options below for resort {_resort.ResortName}.\n");
                var resortsServiceHelper = new ResortsServiceHelper(_console, _resort);
                resortsServiceHelper.AdditionalResortInformationOptions();
            }
            else
            {
                _console.ForegroundColor(DisColors.Red);
                _console.WriteLine("Yes or No was not inputted. No information was updated.");
            }
        }
    }
}