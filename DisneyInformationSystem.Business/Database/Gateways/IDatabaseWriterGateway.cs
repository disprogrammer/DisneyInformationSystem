using DisneyInformationSystem.Business.Database.Records;

namespace DisneyInformationSystem.Business.Database.Gateways
{
    /// <summary>
    /// Database writer gateway interface.
    /// </summary>
    public interface IDatabaseWriterGateway
    {
        /// <summary>
        /// Deletes a row from a table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Delete(GenericRecord record);

        /// <summary>
        /// Inserts a new row into a table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Insert(GenericRecord record);

        /// <summary>
        /// Updates a row in a table.
        /// </summary>
        /// <param name="record">Record.</param>
        void Update(GenericRecord record);
    }
}