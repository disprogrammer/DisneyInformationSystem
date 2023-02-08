using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using System;

namespace DisneyInformationSystem.ConsoleUI.Inserters
{
    /// <summary>
    /// Theme park inserter class.
    /// </summary>
    public class ThemeParkInserter : IInserter
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Resort acronym.
        /// </summary>
        private readonly string _resortAcronym;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeParkInserter"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        /// <param name="databaseReaderGateway">Database reader gateway.</param>
        /// <param name="databaseWriterGateway">Database writer gateway.</param>
        /// <param name="resortAcronym">Resort acronym.</param>
        public ThemeParkInserter(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway, string resortAcronym)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
            _resortAcronym = resortAcronym;
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
                    servicesHelper.InitialMessages("Theme Park");

                    _console.ForegroundColor(DisColors.White);
                    var themeParkAcronym = _console.Prompt("Theme Park Acronym (3 letters): ").ToUpper();
                    if (string.IsNullOrWhiteSpace(themeParkAcronym))
                    {
                        finished = true;
                        break;
                    }

                    var acronymAlreadyInUse = RecordHelper<ThemePark>.AcronymIsAlreadyInUse(_databaseReaderGateway.RetrieveListOfThemeParks(), themeParkAcronym);
                    if (acronymAlreadyInUse)
                    {
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("The provided acronym is already used for another theme park. Please try again.");
                        continue;
                    }

                    ExceptionHandler.CheckIfAcronymIsValid(themeParkAcronym);

                    var themeParkName = _console.Prompt("Theme Park Name: ");
                    if (string.IsNullOrWhiteSpace(themeParkName))
                    {
                        finished = true;
                        break;
                    }

                    var addressOfPark = _console.Prompt("Address of Theme Park: ");
                    if (string.IsNullOrWhiteSpace(addressOfPark))
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

                    var transportation = servicesHelper.RetrieveTransportation();

                    _console.ForegroundColor(DisColors.Yellow);
                    _console.TypeString("For the entries of inputting numbers, 0 is defaulted if nothing is entered.\n");

                    _console.ForegroundColor(DisColors.White);
                    var numberOfLandsInteger = servicesHelper.RetrieveNumber("Number of Lands (number only): ");
                    var numberOfAttractionsInteger = servicesHelper.RetrieveNumber("Number of Attractions (number only): ");
                    var numberOfShopsInteger = servicesHelper.RetrieveNumber("Number of Shops (number only): ");
                    var numberOfRestaurantsInteger = servicesHelper.RetrieveNumber("Number of Restaurants (number only): ");
                    var numberOfToursInteger = servicesHelper.RetrieveNumber("Number of Tours (number only): ");
                    var numberOfRestroomsInteger = servicesHelper.RetrieveNumber("Number of Restrooms (number only): ");
                    var operatingValue = servicesHelper.RetrieveOperatingValue("theme park");
                    var openingDateTime = servicesHelper.RetrieveOpeningDate();
                    var closingDateTime = servicesHelper.RetrieveClosingDate(operatingValue);

                    var themePark = new ThemePark(
                        themeParkAcronym,
                        _resortAcronym,
                        themeParkName,
                        addressOfPark,
                        phoneNumber,
                        transportation,
                        numberOfLandsInteger,
                        numberOfAttractionsInteger,
                        numberOfShopsInteger,
                        numberOfRestaurantsInteger,
                        numberOfToursInteger,
                        numberOfRestroomsInteger,
                        operatingValue,
                        openingDateTime,
                        closingDateTime);

                    _databaseWriterGateway.Insert(themePark);

                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine("The theme park was added to the database successfully!");
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
    }
}