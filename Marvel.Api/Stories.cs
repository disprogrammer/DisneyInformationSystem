using Marvel.Api.Filters;
using Marvel.Api.Results;
using RestSharp;
using System.Configuration;

namespace Marvel.Api
{
    /// <summary>
    /// MarvelRestClient partial class.
    /// </summary>
    public partial class MarvelRestClient
    {
        /// <summary>
        /// Stories URI.
        /// </summary>
        private readonly string StoriesUrlSegment = ConfigurationManager.AppSettings["StoriesUrl"];

        /// <summary>
        /// Fetches lists of comic stories with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetStories(StoryRequestFilter filter)
        {
            var request = new RestRequest(StoriesUrlSegment, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }

        /// <summary>
        /// This method fetches a single comic story resource.
        /// It is the canonical URI for any comic story resource provided by the API.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        public virtual StoryResult GetStory(string storyId)
        {
            var requestUrl = string.Format("{0}/{1}", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<StoryResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic characters appearing in a single story,
        /// with optional filters.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CharacterResult GetStoryCharacters(string storyId, CharacterRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/characters", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCharacterFilter(request, filter);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// Fetches lists of comics in which a specific story appears, with optional filters.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetStoryComics(string storyId, ComicRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/comics", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseComicFilter(request, filter);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic creators whose work appears in a specific story, with optional filters.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CreatorResult GetStoryCreators(string storyId, CreatorRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/creators", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCreatorFilter(request, filter);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// Fetches lists of events in which a specific story appears, with optional filters.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetStoryEvents(string storyId, EventRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/events", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic series in which the specified story takes place.
        /// </summary>
        /// <param name="storyId">Story unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual SeriesResult GetStorySeries(string storyId, SeriesRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/series", StoriesUrlSegment, storyId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseSeriesFilter(request, filter);
            return Execute<SeriesResult>(request);
        }
    }
}