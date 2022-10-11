using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Inserters;
using DisneyInformationSystem.ConsoleUI.Services.Helpers;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Resort hotels service.
    /// </summary>
    public class ResortHotelsService
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

        /// <summary>
        /// Gives the user the option to add, update, or delete a resort hotel.
        /// </summary>
        /// <param name="resort"></param>
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
                        break;

                    case "3":
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
    }
}