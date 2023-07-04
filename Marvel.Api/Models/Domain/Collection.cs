namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Collection model.
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Gets or sets ResourceURI.
        /// </summary>
        public string ResourceURI { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets NAme.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}