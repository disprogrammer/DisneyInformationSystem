namespace DisneyInformationSystem.ConsoleUI.Registrations
{
    /// <summary>
    /// Registration interface.
    /// </summary>
    public interface IRegister<out T>
    {
        /// <summary>
        /// Registers a user or admin.
        /// </summary>
        /// <returns>Model object.</returns>
        public T Register();
    }
}