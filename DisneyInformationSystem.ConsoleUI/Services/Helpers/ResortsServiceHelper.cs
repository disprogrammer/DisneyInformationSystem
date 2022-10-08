using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using System.Linq;

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

        // <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResortsServiceHelper"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        /// <param name="resort">Resort object.</param>
        /// <param name="databaseWriterGateway">Database writer gateway.</param>
        public ResortsServiceHelper(IConsole console, Resort resort, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _resort = resort;
            _databaseWriterGateway = databaseWriterGateway;
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
                    var themeParkService = new ThemeParkService(_console);
                    themeParkService.Options(_resort.PIN);
                    break;

                case "":
                    break;

                default:
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine("This is not a valid option. Please try again.");
                    break;
            }
        }

        /// <summary>
        /// Updates resort field value or throws an exception.
        /// </summary>
        public void UpdateResortValues()
        {
            var doneUpdating = false;
            var exceptionIsThrown = false;
            while (!doneUpdating)
            {
                try
                {
                    _console.WriteLine("Type the field name that you would like to update for it's value.");
                    var decision = _console.Prompt("> ").ToLower();
                    if (string.IsNullOrWhiteSpace(decision))
                    {
                        _console.ForegroundColor(DisColors.Red);
                        _console.WriteLine("You did not provide a field name to update, so no changes were made to the resort.");
                        return;
                    }

                    var upperCaseWordsInDecsion = decision.ToTitleCase();
                    var decisionWithoutSpaces = upperCaseWordsInDecsion.Replace(" ", "");
                    var resortHasProperty = _resort.GetType().GetProperties().Any(property => property.Name == decisionWithoutSpaces);

                    doneUpdating = CheckIfResortHasSelectedProperty(resortHasProperty, decisionWithoutSpaces);
                }
                catch (InvalidPropertyTypeException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    var servicesHelper = new ServicesHelper(_console);
                    servicesHelper.CheckIfFinsihedOrExceptionIsThrown(exceptionIsThrown, doneUpdating);
                }
            }
        }

        /// <summary>
        /// Checks if the selected property is in the resort.
        /// If it is, admin can update the properties value.
        /// Otherwise, it will throw an exception.
        /// </summary>
        /// <param name="resortHasProperty">Resort has property.</param>
        /// <param name="decisionWithoutSpaces">Decision without spaces.</param>
        /// <returns>True if admin is done, false, otherwise.</returns>
        /// <exception cref="InvalidPropertyTypeException">Invalid property type exception.</exception>
        private bool CheckIfResortHasSelectedProperty(bool resortHasProperty, string decisionWithoutSpaces)
        {
            if (resortHasProperty)
            {
                var propertyToUpdate = _resort.GetType().GetProperty(decisionWithoutSpaces);
                var newValue = _console.Prompt($"New Value for {propertyToUpdate.Name}: ");
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine($"No input was provided to change the value of {propertyToUpdate.Name}. No changes will be made.");
                    return true;
                }

                propertyToUpdate.SetValue(_resort, newValue, null);
                _databaseWriterGateway.UpdateResort(_resort);

                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine($"Resort was successfully updated. {propertyToUpdate.Name} new value is: {propertyToUpdate.GetValue(_resort, null)}.");

                _console.ForegroundColor(DisColors.White);
                var continueUpdating = _console.Prompt("Would you like to update another value? (Y/N): ").ToLower();
                if (continueUpdating == "y" || continueUpdating == "yes")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new InvalidPropertyTypeException($"Inputted field name {decisionWithoutSpaces} is not a field in the Resorts table.");
            }
        }
    }
}