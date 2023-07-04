using Marvel.Api.Model.Lists;
using Marvel.Api.Model.Summaries;

namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Series model.
    /// </summary>
    public class Series
    {
        /// <summary>
        /// The unique ID of the series resource.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The canonical title of the series.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A description of the series.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The canonical URL identifier for this resource.
        /// </summary>
        public string ResourceURI { get; set; } = string.Empty;

        /// <summary>
        /// A set of public web site URLs for the resource.
        /// </summary>
        public List<Url> Urls { get; set; } = new List<Url>();

        /// <summary>
        /// The first year of publication for the series.
        /// </summary>
        public string StartYear { get; set; } = string.Empty;

        /// <summary>
        /// The last year of publication for the series (conventionally, 2099 for ongoing series) .,
        /// </summary>
        public string EndYear { get; set; } = string.Empty;

        /// <summary>
        /// The age-appropriateness rating for the series.,
        /// </summary>
        public string Rating { get; set; } = string.Empty;

        /// <summary>
        /// The date the resource was most recently modified.
        /// </summary>
        public string Modified { get; set; } = string.Empty;

        /// <summary>
        /// The representative image for this series.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = new Thumbnail();

        /// <summary>
        /// A resource list containing comics in this series.
        /// </summary>
        public TypeList<ComicSummary> Comics { get; set; } = new TypeList<ComicSummary>();

        /// <summary>
        /// A resource list containing stories which occur in comics in this series.
        /// </summary>
        public TypeList<StorySummary> Stories { get; set; } = new TypeList<StorySummary>();

        /// <summary>
        /// A resource list containing events which take place in comics in this series.
        /// </summary>
        public TypeList<EventSummary> Events { get; set; } = new TypeList<EventSummary>();

        /// <summary>
        /// A resource list containing characters which appear in comics in this series.
        /// </summary>
        public TypeList<CharacterSummary> Characters { get; set; } = new TypeList<CharacterSummary>();

        /// <summary>
        /// A resource list of creators whose work appears in comics in this series.
        /// </summary>
        public TypeList<CreatorSummary> Creators { get; set; } = new TypeList<CreatorSummary>();

        /// <summary>
        /// A summary representation of the series which follows this series.
        /// </summary>
        public SeriesSummary Next { get; set; } = new SeriesSummary();

        /// <summary>
        /// A summary representation of the series which preceded this series.
        /// </summary>
        public SeriesSummary Previous { get; set; } = new SeriesSummary();
    }
}