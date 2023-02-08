using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;

namespace DisneyInformationSystem.ConsoleUI.Deleters
{
    /// <summary>
    /// Deleter class.
    /// </summary>
    public class DeleterBase : IDeleterBase
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
        /// Initializes a new instance of the <see cref="DeleterBase"/> class.
        /// </summary>
        /// <param name="console"></param>
        /// <param name="databaseReaderGateway"></param>
        /// <param name="databaseWriterGateway"></param>
        public DeleterBase(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
        }

        /// <inheritdoc />
        public void DeleteEntertainmentVenues(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void DeleteResortHotels(string resortPin, DateTime closingDate)
        {
            var listOfResortHotels = _databaseReaderGateway.RetrieveResortHotelsByResortID(resortPin);
            foreach (var resortHotel in listOfResortHotels)
            {
                DeleteResortHotel(resortHotel, closingDate);
            }
        }

        /// <inheritdoc />
        public void DeleteThemeParks(string resortPin, DateTime closingDate)
        {
            var listOfThemeParks = _databaseReaderGateway.RetrieveThemeParksByResortID(resortPin);
            foreach (var themePark in listOfThemeParks)
            {
                DeleteThemePark(themePark, closingDate);
            }
        }

        /// <inheritdoc />
        public void DeleteTransportation(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void DeleteWaterParks(string resortPin)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Deletes a single theme park from a resort.
        /// </summary>
        /// <param name="themePark">Theme park.</param>
        /// <param name="closingDate">Closing date.</param>
        public void DeleteThemePark(ThemePark themePark, DateTime closingDate)
        {
            var operatingProperty = themePark.GetType().GetProperty("Operating");
            operatingProperty.SetValue(themePark, false, null);
            var closingProperty = themePark.GetType().GetProperty("ClosingDate");
            closingProperty.SetValue(themePark, closingDate, null);
            _databaseWriterGateway.Update(themePark);

            _console.ForegroundColor(DisColors.Green);
            _console.WriteLine($"Theme Park: {themePark.ParkName} has successfully been updated.\n" +
                $"- The operating value is now {operatingProperty.GetValue(themePark, null)}.\n" +
                $"- The closing date value is not {closingProperty.GetValue(themePark, null)}.");

            // TODO: Delete attractions, restaurants, guest services, etc.
        }

        /// <summary>
        /// Deletes a single resort hotel from a resort.
        /// </summary>
        /// <param name="resortHotel">Resort hotel.</param>
        /// <param name="closingDate">Closing date.</param>
        public void DeleteResortHotel(ResortHotel resortHotel, DateTime closingDate)
        {
            var operatingProperty = resortHotel.GetType().GetProperty("Operating");
            operatingProperty.SetValue(resortHotel, false, null);
            var closingProperty = resortHotel.GetType().GetProperty("ClosingDate");
            closingProperty.SetValue(resortHotel, closingDate, null);
            _databaseWriterGateway.Update(resortHotel);

            _console.ForegroundColor(DisColors.Green);
            _console.WriteLine($"Resort Hotel: {resortHotel.ResortHotelName} has successfully been updated.\n" +
                $"- The operating value is now {operatingProperty.GetValue(resortHotel, null)}.\n" +
                $"- The closing date value is now {closingProperty.GetValue(resortHotel, null)}.");

            // TODO: Delete restaurants, shops, guest services, etc.
        }
    }
}