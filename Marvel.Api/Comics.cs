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
        /// Comics uri.
        /// </summary>
        private readonly string? ComicsUrlSegment = ConfigurationManager.AppSettings["ComicsUrl"];

        /// <summary>
        /// Fetches lists of comics with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetComics(ComicRequestFilter filter)
        {
            var request = new RestRequest(ComicsUrlSegment, Method.Get);
            ParseComicFilter(request, filter);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// This method fetches a single comic resource.
        /// It is the canonical URI for any comic resource provided by the API.
        /// </summary>
        /// <param name="comicId">Comic unique identifier</param>
        public virtual ComicResult GetComic(string comicId)
        {
            var requestUrl = string.Format("{0}/{1}", ComicsUrlSegment, comicId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic creators whose work appears in a specific comic, with optional filters.
        /// </summary>
        /// <param name="comicId">Comic unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CreatorResult GetComicCreators(string comicId, CreatorRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/creators", ComicsUrlSegment, comicId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCreatorFilter(request, filter);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// Fetches lists of characters which appear in a specific comic with optional filters.
        /// </summary>
        /// <param name="comicId">Comic unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CharacterResult GetComicCharacters(string comicId, CharacterRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/characters", ComicsUrlSegment, comicId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCharacterFilter(request, filter);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// Fetches lists of events in which a specific comic appears, with optional filters.
        /// </summary>
        /// <param name="comicId">Comic unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetComicEvents(string comicId, EventRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/events", ComicsUrlSegment, comicId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic stories in a specific comic issue, with optional filters.
        /// </summary>
        /// <param name="comicId">Comic unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetComicStories(string comicId, StoryRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/stories", ComicsUrlSegment, comicId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }
    }
}