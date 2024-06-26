﻿namespace Marvel.Api
{
    /// <summary>
    /// DataContainer model.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class DataContainer<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets Offset.
        /// </summary>
        public string Offset { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Limit.
        /// </summary>
        public string Limit { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Total.
        /// </summary>
        public string Total { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Count.
        /// </summary>
        public string Count { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Results.
        /// </summary>
        public List<T> Results { get; set; } = new List<T>();
    }
}