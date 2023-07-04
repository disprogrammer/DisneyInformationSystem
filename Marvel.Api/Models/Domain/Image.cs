namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Image model.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets Path.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Extensions.
        /// </summary>
        public string Extension { get; set; } = string.Empty;
    }
}