namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Url model.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets URL.
        /// </summary>
        public string URL { get; set; } = string.Empty;
    }
}