namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Person record for Admin and User tables.
    /// </summary>
    /// <param name="PIN">Personal Identification Number.</param>
    /// <param name="EmailAddress">Email address.</param>
    /// <param name="Password">Password.</param>
    public record Person(string PIN, string EmailAddress, string Password) : GenericRecord(PIN)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person() : this(default, default, default) { }
    }
}