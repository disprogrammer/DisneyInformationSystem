using System;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.MSTests
{
    /// <summary>
    /// Business test helper class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class BusinessTestHelper
    {
        /// <summary>
        /// Writes fail message for Assert.Fail.
        /// </summary>
        /// <param name="exceptionName">Exception name.</param>
        /// <returns>Fail message.</returns>
        public static string FailMessage(string exceptionName)
        {
            return $"We were expecting no exception to be thrown, but instead we got: {exceptionName}";
        }
    }
}