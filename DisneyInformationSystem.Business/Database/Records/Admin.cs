namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Admin record.
    /// </summary>
    /// <param name="PIN">Personal identification number.</param>
    /// <param name="AdminTypeCode">Admin type code.</param>
    /// <param name="FirstName">First name.</param>
    /// <param name="LastName">Last name.</param>
    /// <param name="EmailAddress">Email address.</param>
    /// <param name="Password">Password.</param>
    /// <param name="AssessmentScore">Assessment score.</param>
    public record Admin(string PIN, string AdminTypeCode, string FirstName, string LastName, string EmailAddress, string Password, int AssessmentScore) : Person(PIN, EmailAddress, Password)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Admin() : this(default, default, default, default, default, default, default) { }
    }
}