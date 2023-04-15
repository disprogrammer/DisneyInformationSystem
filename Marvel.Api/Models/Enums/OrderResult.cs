using System.ComponentModel;

namespace Marvel.Api.Models.Enums
{
    /// <summary>
    /// OrderResult enum.
    /// </summary>
    [Flags]
    public enum OrderResult
    {
        /// <summary>
        /// NameAscending
        /// </summary>
        [Description("name")]
        NameAscending = 0x00,

        /// <summary>
        /// ModifiedAscending
        /// </summary>
        [Description("modified")]
        ModifiedAscending = 0x01,

        /// <summary>
        /// NameDescending
        /// </summary>
        [Description("-name")]
        NameDescending = 0x02,

        /// <summary>
        /// ModifiedDescending
        /// </summary>
        [Description("-modified")]
        ModifiedDescending = 0x04
    }
}