using Marvel.Api.Model.Lists;
using Marvel.Api.Model.Summaries;

namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Creator model.
    /// </summary>
    public class Creator
    {
        /// <summary>
        /// The unique ID of the creator resource.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The first name of the creator.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The middle name of the creator.
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the creator.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The suffix or honorific for the creator.
        /// </summary>
        public string Suffix { get; set; } = string.Empty;

        /// <summary>
        /// The full name of the creator
        /// (a space-separated concatenation of the above four fields).
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The date the resource was most recently modified.
        /// </summary>
        public string Modified { get; set; } = string.Empty;

        /// <summary>
        /// The canonical URL identifier for this resource.
        /// </summary>
        public string ResourceURI { get; set; } = string.Empty;

        /// <summary>
        /// A set of public web site URLs for the resource.
        /// </summary>
        public List<Url> Urls { get; set; } = new List<Url>();

        /// <summary>
        /// The representative image for this creator.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = new Thumbnail();

        /// <summary>
        /// A resource list containing the series which feature work by this creator.
        /// </summary>
        public TypeList<SeriesSummary> Series { get; set; } = new TypeList<SeriesSummary>();

        /// <summary>
        /// A resource list containing the stories which feature work by this creator.
        /// </summary>
        public TypeList<StorySummary> Stories { get; set; } = new TypeList<StorySummary>();

        /// <summary>
        /// A resource list containing the comics which feature work by this creator.
        /// </summary>
        public TypeList<ComicSummary> Comics { get; set; } = new TypeList<ComicSummary>();

        /// <summary>
        /// A resource list containing the events which feature work by this creator.
        /// </summary>
        public TypeList<EventSummary> Events { get; set; } = new TypeList<EventSummary>();
    }
}