using Marvel.Api.Model.Lists;
using Marvel.Api.Model.Summaries;

namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Event model.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The unique ID of the event resource.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the event.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The canonical URL identifier for this resource.
        /// </summary>
        public string ResourceURI { get; set; } = string.Empty;

        /// <summary>
        /// A set of public web site URLs for the event.
        /// </summary>
        public List<Url> Urls { get; set; } = new List<Url>();

        /// <summary>
        /// The date the resource was most recently modified.
        /// </summary>
        public string Modified { get; set; } = string.Empty;

        /// <summary>
        /// The date of publication of the first issue in this event.
        /// </summary>
        public string Start { get; set; } = string.Empty;

        /// <summary>
        /// The date of publication of the last issue in this event.
        /// </summary>
        public string End { get; set; } = string.Empty;

        /// <summary>
        /// The representative image for this event.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = new Thumbnail();

        /// <summary>
        /// A resource list containing the comics in this event.
        /// </summary>
        public TypeList<ComicSummary> Comics { get; set; } = new TypeList<ComicSummary>();

        /// <summary>
        /// A resource list containing the stories in this event.
        /// </summary>
        public TypeList<StorySummary> Stories { get; set; } = new TypeList<StorySummary>();

        /// <summary>
        /// A resource list containing the series in this event.
        /// </summary>
        public TypeList<SeriesSummary> Series { get; set; } = new TypeList<SeriesSummary>();

        /// <summary>
        /// A resource list containing the characters which appear in this event.
        /// </summary>
        public TypeList<CharacterSummary> Characters { get; set; } = new TypeList<CharacterSummary>();

        /// <summary>
        /// A resource list containing creators whose work appears in this event.
        /// </summary>
        public TypeList<CreatorSummary> Creators { get; set; } = new TypeList<CreatorSummary>();

        /// <summary>
        /// A summary representation of the event which follows this event.
        /// </summary>
        public EventSummary Next { get; set; } = new EventSummary();

        /// <summary>
        /// A summary representation of the event which preceded this event.
        /// </summary>
        public EventSummary Previous { get; set; } = new EventSummary();
    }
}