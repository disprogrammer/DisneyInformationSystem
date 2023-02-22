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
    /// Resort hotel inserter class.
    /// </summary>
    public class ResortHotelInserter : IInserter
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
        /// Initializes a new instance of the <see cref="ResortHotelInserter"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        /// <param name="databaseReaderGateway">Database reader gateway.</param>
        /// <param name="databaseWriterGateway">Database writer gateway.</param>
        /// <param name="resortAcronym">Resort acronym.</param>
        public ResortHotelInserter(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway, string resortAcronym)
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
                    servicesHelper.InitialMessages("Resort Hotel");

                    _console.ForegroundColor(DisColors.White);
                    var resortHotelAcronym = _console.Prompt("Resort Hotel Acronym (3 letters): ").ToUpper();
                    if (string.IsNullOrWhiteSpace(resortHotelAcronym))
                    {
                        finished = true;
                        break;
                    }

                    var acronymAlreadyInUse = RecordHelper<ResortHotel>.AcronymIsAlreadyInUse(_databaseReaderGateway.RetrieveListOfResortHotels(), resortHotelAcronym);
                    if (acronymAlreadyInUse)
                    {
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("The provided acronym is already used for another resort hotel. Please try again.");
                        continue;
                    }

                    ExceptionHandler.CheckIfAcronymIsValid(resortHotelAcronym);

                    var resortHotelName = _console.Prompt("Resort Hotel Name: ");
                    if (string.IsNullOrWhiteSpace(resortHotelName))
                    {
                        finished = true;
                        break;
                    }
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