namespace DisneyInformationSystem.ConsoleUI.Deleters
{
    /// <summary>
    /// Deleter base interface.
    /// </summary>
    public interface IDeleterBase
    {
        /// <summary>
        /// Sets theme parks within deleted resort to not operating.
        /// </summary>
        /// <param name="resortPin">Resort pin.</param>
        void DeleteThemeParks(string resortPin);

        /// <summary>
        /// Sets water parks within deleted resort to not operating.
        /// </summary>
        /// <param name="resortPin">Resort pin.</param>
        void DeleteWaterParks(string resortPin);

        /// <summary>
        /// Sets resort hotels within deleted resort to not operating.
        /// </summary>
        /// <param name="resortPin">Resort pin.</param>
        void DeleteResortHotels(string resortPin);

        /// <summary>
        /// Sets entertainment venues within deleted resort to not operating.
        /// </summary>
        /// <param name="resortPin">Resort pin.</param>
        void DeleteEntertainmentVenues(string resortPin);

        /// <summary>
        /// Sets transportation within deleted resort to not operating.
        /// </summary>
        /// <param name="resortPin">Resort pin.</param>
        void DeleteTransportation(string resortPin);
    }
}