namespace Marvel.Api
{
    /// <summary>
    /// ResultBase class.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class ResultBase<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Copyright.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets AttributionText.
        /// </summary>
        public string AttributionText { get; set; }

        /// <summary>
        /// Gets or sets AttributionHtml.
        /// </summary>
        public string AttributionHtml { get; set; }

        /// <summary>
        /// Gets or sets Data.
        /// </summary>
        public DataContainer<T> Data { get; set; }

        /// <summary>
        /// Gets or sets Etag.
        /// </summary>
        public string Etag { get; set; }
    }
}