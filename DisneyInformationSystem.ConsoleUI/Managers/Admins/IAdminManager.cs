namespace DisneyInformationSystem.ConsoleUI.Managers.Admins
{
    /// <summary>
    /// Admin Manager Interface.
    /// </summary>
    internal interface IAdminManager
    {
        /// <summary>
        /// Gives the options of what the admin can update and tasks they can perform.
        /// </summary>
        void UpdateCore();
    }
}