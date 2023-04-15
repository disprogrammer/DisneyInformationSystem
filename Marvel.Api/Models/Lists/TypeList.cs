using System.Collections.Generic;

namespace Marvel.Api.Model.Lists
{
    /// <summary>
    /// TypeList model.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class TypeList<T>
    {
        /// <summary>
        /// Gets or sets Available.
        /// </summary>
        public string Available { get; set; }

        /// <summary>
        /// Gets or sets Returned.
        /// </summary>
        public string Returned { get; set; }

        /// <summary>
        /// Gets or sets CollectionURI.
        /// </summary>
        public string CollectionURI { get; set; }

        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public List<T> Items { get; set; }
    }
}