using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using DisneyInformationSystem.ConsoleUI.Helpers;
using DisneyInformationSystem.ConsoleUI.Inserters;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Resort hotels service.
    /// </summary>
    public class ResortHotelsService : IResortServiceBase
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="DatabaseReaderGateway"/> class.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResortHotelsService"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ResortHotelsService(IConsole console, IDatabaseReaderGateway databaseReaderGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
        }

        /// <inheritdoc />
        public void Options(Resort resort)
        {
            var finished = false;
            while (!finished)
            {
                var servicesHelper = new ServicesHelper(_console);
                var decision = servicesHelper.RetrieveServiceDecision("===== Resort Hotels Service =====");

                switch (decision)
                {
                    case "1":
                        var resortHotelInserter = new ResortHotelInserter(_console, _databaseReaderGateway, new DatabaseWriterGateway(), resort.PIN);
                        resortHotelInserter.Add();
                        break;

                    case "2":
                        var resortHotelToUpdate = RetrieveResortHotel(resort);
                        if (resortHotelToUpdate != null)
                        {
                            var recordPropertiesAndValues = RecordHelper<ResortHotel>.RetrieveListOfPropertiesAndValues(resortHotelToUpdate);
                            servicesHelper.UpdateRecord(resortHotelToUpdate, recordPropertiesAndValues);
                        }
                        else
                        {
                            servicesHelper.NotValidMessage("resort hotel");
                        }
                        break;

                    case "3":
                        DeleteResortHotel(resort, servicesHelper);
                        break;

                    case "":
                        finished = true;
                        break;

                    default:
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("This is not a valid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Deletes a resort hotel record.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        /// <param name="servicesHelper">Services helper.</param>
        private void DeleteResortHotel(Resort resort, ServicesHelper servicesHelper)
        {
            var resortHotelToDelete = RetrieveResortHotel(resort);
            if (resortHotelToDelete != null)
            {
                var deleter = new DeleterBase(_console, _databaseReaderGateway, new DatabaseWriterGateway());
                var closingDate = servicesHelper.RetrieveClosingDate(false);
                deleter.DeleteResortHotel(resortHotelToDelete, closingDate);
            }
            else
            {
                servicesHelper.NotValidMessage("resort hotel");
            }
        }

        /// <summary>
        /// Retrieves the resort hotel to use.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        /// <returns>Resort hotel record.</returns>
        private ResortHotel RetrieveResortHotel(Resort resort)
        {
            var resortHotels = _databaseReaderGateway.RetrieveResortHotelsByResortID(resort.PIN);
            _console.ForegroundColor(DisColors.Yellow);
            _console.WriteLine("\nSelect a resort hotel below.");
            _console.ForegroundColor(DisColors.White);
            foreach (var resortHotel in resortHotels)
            {
                _console.WriteLine($"- {resortHotel.ResortHotelName}");
            }

            var resortHotelDedcision = _console.Prompt(">> ").ToLower();
            return resortHotels.Find(resortHotel => resortHotel.ResortHotelName.ToLower().Contains(resortHotelDedcision));
        }
    }
}