using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services.Helpers;

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
        /// Use of the <see cref="DatabaseWriterGateway"/> class.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResortHotelsService"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ResortHotelsService(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void Options(Resort resort)
        {
            var finished = false;
            while (!finished)
            {
                var resortsServiceHelper = new ResortsServiceHelper(_console, resort);
                var decision = resortsServiceHelper.RetrieveServiceDecision("===== Resort Hotels Service =====");

                switch (decision)
                {
                    case "1":
                        break;

                    case "2":
                        UpdateResortHotel(resort);
                        break;

                    case "3":
                        DeleteResortHotel(resort);
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
        /// Updates a resort hotel record.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        private void UpdateResortHotel(Resort resort)
        {
            
        }

        /// <summary>
        /// Deletes a resort hotel record.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        private void DeleteResortHotel(Resort resort)
        {
            
        }

        /// <summary>
        /// Retrieves the resort hotel to use.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        /// <returns>Resort hotel record.</returns>
        private ResortHotel RetrieveResortHotel(Resort resort)
        {
            return new ResortHotel();
        }
    }
}