namespace DisneyInformationSystem.Business.Database.Records
{
    /// <summary>
    /// Generic record for database tables.
    /// </summary>
    /// <param name="PIN">Personal identification number.</param>
    public record GenericRecord(string PIN)
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GenericRecord() : this(string.Empty) { }
    }
}