using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Updaters;
using System;
using System.Collections.Generic;

namespace DisneyInformationSystem.ConsoleUI.Helpers
{
    /// <summary>
    /// Services helper class.
    /// </summary>
    public class ServicesHelper
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesHelper"/> class.
        /// </summary>
        /// <param name="console">Console interface.</param>
        public ServicesHelper(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Retrieves the operating value.
        /// </summary>
        /// <param name="service">Service string.</param>
        /// <returns>True if value is 'Y'; otherwise false.</returns>
        public bool RetrieveOperatingValue(string service)
        {
            _console.WriteLine($"Is the {service} in operation (Y/N)? NOTE: Not providing 'Y' will result in the field defaulting to FALSE.");
            var operatingInput = _console.Prompt("> ").ToLower();
            var operatingValue = operatingInput == "y";
            return operatingValue;
        }

        /// <summary>
        /// Retrieves the opening date. Throws exception if format is not correct.
        /// </summary>
        /// <returns>Opening date.</returns>
        /// <exception cref="FormatException">Format exception.</exception>
        public DateTime RetrieveOpeningDate()
        {
            var openingDate = _console.Prompt("Opening Date (YYYY-MM-DD): ");
            return ExceptionHandler.CheckDateTime(openingDate);
        }

        /// <summary>
        /// Retrieves the closing date. Throws exception if the format is not correct.
        /// </summary>
        /// <param name="operating">Operating value.</param>
        /// <returns>DateTime value.</returns>
        public DateTime RetrieveClosingDate(bool operating)
        {
            if (operating)
            {
                return DateTime.MaxValue;
            }

            var closingDate = _console.Prompt("Closing Date (YYYY-MM-DD): ");
            return ExceptionHandler.CheckDateTime(closingDate);
        }

        /// <summary>
        /// Retrieves a integer value if the input is a number.
        /// </summary>
        /// <param name="prompt">Prompt.</param>
        /// <returns>Integer if input is number, exception if not.</returns>
        public int RetrieveNumber(string prompt)
        {
            var number = _console.Prompt(prompt);
            return ExceptionHandler.CheckIfInputIsNumber(number);
        }

        /// <summary>
        /// Checks if an exception is thrown or if the admin is finished.
        /// </summary>
        /// <param name="exceptionIsThrown">Exception is thrown exception.</param>
        /// <param name="finished">Finished boolean.</param>
        public void CheckIfFinsihedOrExceptionIsThrown(bool exceptionIsThrown, bool finished)
        {
            if (exceptionIsThrown)
            {
                finished = false;
                _console.WriteLine("Please try again.");
            }

            if (finished)
            {
                _console.ForegroundColor(DisColors.Green);
                _console.WriteLine("Thank you for your contributions to the Disney Information System!");
            }
        }

        /// <summary>
        /// Retrieves the service decision to add, update, or delete a certain record.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <returns>User input.</returns>
        public string RetrieveServiceDecision(string title)
        {
            _console.Clear();
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine(title);

            _console.ForegroundColor(DisColors.Yellow);
            _console.WriteLine("Select an option below that you would like to do.");

            _console.ForegroundColor(DisColors.White);
            return _console.Prompt("1. Add\n" +
                "2. Update\n" +
                "3. Delete\n" +
                ">> ");
        }

        /// <summary>
        /// Updates theme park properties.
        /// </summary>
        /// <param name="recordToUpdate">Record to update.</param>
        /// <param name="recordPropertiesAndValues">List of record properties and values..</param>
        public void UpdateRecord(GenericRecord recordToUpdate, List<string> recordPropertiesAndValues)
        {
            _console.ForegroundColor(DisColors.White);
            foreach (var propertyValuePair in recordPropertiesAndValues)
            {
                _console.WriteLine(propertyValuePair);
            }

            var updater = new Updater(_console, recordToUpdate, new DatabaseWriterGateway());
            updater.Update();
        }

        /// <summary>
        /// Prints message that not a valid record was selected.
        /// </summary>
        /// <param name="type">Record type.</param>
        public void NotValidMessage(string type)
        {
            _console.ForegroundColor(DisColors.Red);
            _console.WriteLine($"A valid {type} was not selected. Please try again.");
        }

        /// <summary>
        /// Shows the initial messages for adding a record.
        /// </summary>
        public void InitialMessages(string recordType)
        {
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine($"\n===== Adding {recordType} =====");

            _console.ForegroundColor(DisColors.Yellow);
            _console.TypeString($"Provide the information below to add a {recordType.ToLower()} to the database.\n");

            _console.ForegroundColor(DisColors.DarkGray);
            _console.WriteLine("If you do not provide any information for the data fields, you will lose your inputs.");
        }

        /// <summary>
        /// Retrieves the transportation to the .
        /// </summary>
        /// <returns>Transportation string.</returns>
        public string RetrieveTransportation()
        {
            _console.ForegroundColor(DisColors.Yellow);
            _console.WriteLine("\nFor transportation, separate each type of transportation by a comma.");
            _console.WriteLine("NOTE: Not providing anything will set it to 'Car' only.");

            _console.ForegroundColor(DisColors.White);
            var transportation = _console.Prompt("Transportation: ");
            if (string.IsNullOrWhiteSpace(transportation))
            {
                transportation = "Car";
            }

            return transportation;
        }
    }
}