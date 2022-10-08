namespace DisneyInformationSystem.ConsoleUI.Services
{
    /// <summary>
    /// Service base interface.
    /// </summary>
    internal interface IServiceBase
    {
        /// <summary>
        /// Allows admin to add data to selected database table.
        /// </summary>
        void Add();

        /// <summary>
        /// Allows admin to remove data from a selected database table.
        /// </summary>
        void Delete();

        /// <summary>
        /// Allows admin to update data in a selected database table.
        /// </summary>
        void Update();
    }
}