using DisneyInformationSystem.Business.Database.Records;

namespace DisneyInformationSystem.ConsoleUI.Updaters
{
    /// <summary>
    /// Updater interface.
    /// </summary>
    internal interface IUpdater
    {
        /// <summary>
        /// Updates information for the give record.
        /// </summary>
        public void Update();
    }
}