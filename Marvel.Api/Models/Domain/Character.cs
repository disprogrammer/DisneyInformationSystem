using Marvel.Api.Model.Lists;
using Marvel.Api.Model.Summaries;

namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Character model.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// The unique ID of the character resource.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A short bio or description of the character.
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
        /// The representative image for this character.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = new Thumbnail();

        /// <summary>
        /// A resource list containing comics which feature this character.
        /// </summary>
        public TypeList<ComicSummary> Comics { get; set; } = new TypeList<ComicSummary>();

        /// <summary>
        /// A resource list of stories in which this character appears.
        /// </summary>
        public TypeList<StorySummary> Stories { get; set; } = new TypeList<StorySummary>();

        /// <summary>
        /// A resource list of events in which this character appears.
        /// </summary>
        public TypeList<EventSummary> Events { get; set; } = new TypeList<EventSummary>();

        /// <summary>
        /// A resource list of series in which this character appears.
        /// </summary>
        public TypeList<SeriesSummary> Series { get; set; } = new TypeList<SeriesSummary>();
    }
}