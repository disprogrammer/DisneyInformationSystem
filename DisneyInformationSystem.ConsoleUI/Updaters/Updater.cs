using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Updaters
{
    /// <summary>
    /// Updater class.
    /// </summary>
    public class Updater : IUpdater
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Record.
        /// </summary>
        private readonly GenericRecord _record;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="Updater"/> class.
        /// </summary>
        /// <param name="console"></param>
        /// <param name="record">Record to update.</param>
        public Updater(IConsole console, GenericRecord record, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _record = record;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void Update()
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
                        _console.WriteLine("You did not provide a field name to update, so no changes were made to the record.");
                        return;
                    }

                    var upperCaseWordsInDecsion = decision.ToTitleCase();
                    var decisionWithoutSpaces = upperCaseWordsInDecsion.Replace(" ", "");
                    var recordHasProperty = _record.GetType().GetProperties().ToList().Exists(property => property.Name == decisionWithoutSpaces);

                    doneUpdating = CheckIfPropertyIsValid(recordHasProperty, decisionWithoutSpaces);
                }
                catch (InvalidPropertyTypeException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    var servicesHelper = new ServicesHelper(_console);
                    servicesHelper.CheckIfFinishedOrExceptionIsThrown(exceptionIsThrown, doneUpdating);
                }
            }
        }

        /// <summary>
        /// Checks if the selected property is in the record.
        /// If it is, admin can update the properties value.
        /// Otherwise, it will throw an exception.
        /// </summary>
        /// <param name="recordHasProperty">Record has property.</param>
        /// <param name="decisionWithoutSpaces">Decision without spaces.</param>
        /// <returns>True if admin is done, false, otherwise.</returns>
        /// <exception cref="InvalidPropertyTypeException">Invalid property type exception.</exception>
        private bool CheckIfPropertyIsValid(bool recordHasProperty, string decisionWithoutSpaces)
        {
            if (recordHasProperty)
            {
                var propertyToUpdate = _record.GetType().GetProperty(decisionWithoutSpaces);
                var newValue = _console.Prompt($"New Value for {propertyToUpdate.Name}: ");
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine($"No input was provided to change the value of {propertyToUpdate.Name}. No changes will be made.");
                    return true;
                }

                propertyToUpdate.SetValue(_record, newValue, null);
                _databaseWriterGateway.Update(_record);

                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine($"Record was successfully updated. {propertyToUpdate.Name} new value is: {propertyToUpdate.GetValue(_record, null)}.");

                _console.ForegroundColor(DisColors.White);
                var continueUpdating = _console.Prompt("Would you like to update another value? (Y/N): ").ToLower();
                return continueUpdating == "y" || continueUpdating == "yes";
            }
            else
            {
                throw new InvalidPropertyTypeException($"Inputted field name {decisionWithoutSpaces} is not a field in the table.");
            }
        }
    }
}