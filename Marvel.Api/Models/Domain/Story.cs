using Marvel.Api.Model.Lists;
using Marvel.Api.Model.Summaries;

namespace Marvel.Api.Models.Domain
{
    /// <summary>
    /// Story model.
    /// </summary>
    public class Story
    {
        /// <summary>
        /// The unique ID of the story resource.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The story title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A short description of the story.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The canonical URL identifier for this resource.
        /// </summary>
        public string ResourceURI { get; set; } = string.Empty;

        /// <summary>
        /// The story type e.g. interior story, cover, text story.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The date the resource was most recently modified.
        /// </summary>
        public string Modified { get; set; } = string.Empty;

        /// <summary>
        /// The representative image for this story.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = new Thumbnail();

        /// <summary>
        /// A resource list containing comics in which this story takes place.
        /// </summary>
        public TypeList<ComicSummary> Comics { get; set; } = new TypeList<ComicSummary>();

        /// <summary>
        /// A resource list containing series in which this story appears.
        /// </summary>
        public TypeList<SeriesSummary> Series { get; set; } = new TypeList<SeriesSummary>();

        /// <summary>
        /// A resource list of the events in which this story appears.
        /// </summary>
        public TypeList<EventSummary> Events { get; set; } = new TypeList<EventSummary>();

        /// <summary>
        /// A resource list of characters which appear in this story.
        /// </summary>
        public TypeList<CharacterSummary> Characters { get; set; } = new TypeList<CharacterSummary>();

        /// <summary>
        /// A resource list of creators who worked on this story.
        /// </summary>
        public TypeList<CreatorSummary> Creators { get; set; } = new TypeList<CreatorSummary>();

        /// <summary>
        /// A summary representation of the issue in which this story was originally published.
        /// </summary>
        public StorySummary OriginalIssue { get; set; } = new StorySummary();
    }
}