using DisneyInformationSystem.Business.Database.Records;

namespace DisneyInformationSystem.Business.Database.Writers
{
    /// <summary>
    /// Database writer interface.
    /// </summary>
    internal interface IDatabaseWriter
    {
        /// <summary>
        /// Deletes a certain record from that particular record's database table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Delete(GenericRecord record);

        /// <summary>
        /// Inserts a certain record into that particular record's database table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Insert(GenericRecord record);

        /// <summary>
        /// Updates a certain record in that particular record's database table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Update(GenericRecord record);
    }
}