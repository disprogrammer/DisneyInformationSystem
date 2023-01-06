using DisneyInformationSystem.Business.Database.Records;

namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Service base interface.
    /// </summary>
    internal interface IResortServiceBase
    {
        /// <summary>
        /// Allows admin to decide what to do with a particular record.
        /// </summary>
        /// <param name="resort">Resort record.</param>
        void Options(Resort resort);
    }
}