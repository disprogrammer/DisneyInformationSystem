using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;

namespace DisneyInformationSystem.ConsoleUI.Services.Helpers
{
    /// <summary>
    /// Resorts service helper class.
    /// </summary>
    public class ResortsServiceHelper
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Resort object.
        /// </summary>
        private readonly Resort _resort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResortsServiceHelper"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        /// <param name="resort">Resort object.</param>
        public ResortsServiceHelper(IConsole console, Resort resort)
        {
            _console = console;
            _resort = resort;
        }

        /// <summary>
        /// Shows the other options that the admin can make.
        /// </summary>
        public void AdditionalResortInformationOptions()
        {
            _console.ForegroundColor(DisColors.White);
            _console.WriteLine("1. Theme Park\n" +
                "2. Resort Hotels\n" +
                "3. Water Park\n" +
                "4. Entertainment Venues\n" +
                "5. Attraction\n" +
                "6. Restaurant\n" +
                "7. Shop\n" +
                "8. Shows\n" +
                "9. Golf Course\n" +
                "10. Tours\n" +
                "11. Guest Services");
            var decision = _console.Prompt("> ");

            switch (decision)
            {
                case "1":
                    var themeParkService = new ThemeParkService(_console, new DatabaseReaderGateway(), new DatabaseWriterGateway());
                    themeParkService.Options(_resort);
                    break;

                case "":
                    break;

                default:
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine("This is not a valid option. Please try again.");
                    break;
            }
        }
    }
}