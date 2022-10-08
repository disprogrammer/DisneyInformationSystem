namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// User record.
    /// </summary>
    /// <param name="PIN">Personal identification number.</param>
    /// <param name="FirstName">First name.</param>
    /// <param name="LastName">Last name.</param>
    /// <param name="PhoneNumber">Phone number.</param>
    /// <param name="EmailAddress">Email address.</param>
    /// <param name="Password">Password.</param>
    /// <param name="HomeAddress">Home address.</param>
    public record User(string PIN, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string Password, string HomeAddress) : Person(PIN, EmailAddress, Password)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public User() : this(default, default, default, default, default, default, default) { }
    }
}