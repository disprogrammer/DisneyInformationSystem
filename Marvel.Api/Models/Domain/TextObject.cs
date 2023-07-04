namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// TextObject model.
    /// </summary>
    public class TextObject
    {
        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}