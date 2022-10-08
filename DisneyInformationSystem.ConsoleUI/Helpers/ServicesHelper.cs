using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;

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
            return CheckDateTime(openingDate);
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
            return CheckDateTime(closingDate);
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
        /// Checks if the date string is a valid date.
        /// </summary>
        /// <param name="dateString">Date string.</param>
        /// <returns>Date time if no exception is thrown.</returns>
        /// <exception cref="FormatException">Format exception.</exception>
        private static DateTime CheckDateTime(string dateString)
        {
            var isValidDateTime = DateTime.TryParse(dateString, out var dateTime);
            if (!isValidDateTime)
            {
                throw new FormatException("Format for date was invalid. Must be YYYY-MM-DD.");
            }

            return dateTime;
        }
    }
}