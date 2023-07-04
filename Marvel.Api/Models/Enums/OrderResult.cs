using System.ComponentModel;

namespace Marvel.Api.Models.Enums
{
    /// <summary>
    /// OrderResult enum.
    /// </summary>
    [Flags]
    public enum OrderResults
    {
        /// <summary>
        /// NameAscending
        /// </summary>
        [Description("name")]
        NameAscending = 0x01,

        /// <summary>
        /// ModifiedAscending
        /// </summary>
        [Description("modified")]
        ModifiedAscending = 0x02,

        /// <summary>
        /// NameDescending
        /// </summary>
        [Description("-name")]
        NameDescending = 0x03,

        /// <summary>
        /// ModifiedDescending
        /// </summary>
        [Description("-modified")]
        ModifiedDescending = 0x04
    }
}