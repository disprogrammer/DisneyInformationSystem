using System.Diagnostics.CodeAnalysis;

namespace Testing.Shared
{
    /// <summary>
    /// Assert message class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AssertMessage
    {
        /// <summary>
        /// Expect values to be equal assert message.
        /// </summary>
        public const string ExpectValuesToBeEqual = "We were expecting the values to be equal, but they were not.";

        /// <summary>
        /// Expect value to be returned assert message.
        /// </summary>
        public const string ExpectValueReturn = "We were expecting a value to be returned, but one was not.";

        /// <summary>
        /// Expect null value assert message.
        /// </summary>
        public const string ExpectNullValue = "We were expecting the value to be null, but it was not.";

        /// <summary>
        /// Expect not null value assert message.
        /// </summary>
        public const string ExpectNotNullValue = "We were expecting the value to not be null, but it was.";

        /// <summary>
        /// Expect true assert message.
        /// </summary>
        public const string ExpectTrue = "We were expecting the value or expression to be true, but it was not.";

        /// <summary>
        /// Expect false assert message.
        /// </summary>
        public const string ExpectFalse = "We were expecting the value or express to be false, but it was not.";
    }
}