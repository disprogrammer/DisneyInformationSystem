namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// ComicPrice model.
    /// </summary>
    public class ComicPrice
    {
        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Price.
        /// </summary>
        public string Price { get; set; } = string.Empty;
    }
}