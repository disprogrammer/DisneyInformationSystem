using System.Collections.Generic;

namespace DisneyInformationSystem.Business.Database.Readers
{
    /// <summary>
    /// Database reader interface.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public interface IDatabaseReader<T>
    {
        /// <summary>
        /// Connects to the database and reads the stored procedure to get a single row from the table.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name.</param>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Object.</returns>
        T GetByEmailAddress(string storedProcedureName, string parameter);

        /// <summary>
        /// Connects to the database and reads the stored procedure to get a single row from the table.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name.</param>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Object.</returns>
        T GetById(string storedProcedureName, string parameter);

        /// <summary>
        /// Connects to the database and reads the stored procedure to get a single row from the table.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name.</param>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Object.</returns>
        T GetByName(string storedProcedureName, string parameter);

        /// <summary>
        /// Connects to the database and reads the stored procedure to get all from certain table.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name.</param>
        /// <returns>List of objects.</returns>
        List<T> GetAll(string storedProcedureName);
    }
}