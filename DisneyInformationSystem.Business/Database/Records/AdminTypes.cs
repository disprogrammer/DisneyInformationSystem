namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Admin Type record.
    /// </summary>
    /// <param name="ID">Identification.</param>
    /// <param name="AdminType">Admin type.</param>
    public record AdminTypes(string ID, string AdminType)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AdminTypes() : this(default, default) { }
    }
}