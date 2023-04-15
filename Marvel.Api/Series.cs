using Marvel.Api.Filters;
using Marvel.Api.Results;
using RestSharp;
using System.Configuration;

namespace Marvel.Api
{
    /// <summary>
    /// MarvelRestClient class.
    /// </summary>
    public partial class MarvelRestClient
    {
        /// <summary>
        /// Series uri.
        /// </summary>
        private readonly string SeriesUrlSegment = ConfigurationManager.AppSettings["SeriesUrl"];

        /// <summary>
        /// Fetches lists of comic series with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual SeriesResult GetSeries(SeriesRequestFilter filter)
        {
            var request = new RestRequest(SeriesUrlSegment, Method.Get);
            ParseSeriesFilter(request, filter);
            return Execute<SeriesResult>(request);
        }

        /// <summary>
        /// This method fetches a single comic series resource.
        /// It is the canonical URI for any comic series resource provided by the API.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        public virtual SeriesResult GetSeries(string seriesId)
        {
            var requestUrl = string.Format("{0}/{1}", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<SeriesResult>(request);
        }

        /// <summary>
        /// Fetches lists of characters which appear in specific series, with optional filters.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CharacterResult GetSeriesCharacters(string seriesId, CharacterRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/characters", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCharacterFilter(request, filter);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// Fetches lists of comics which are published as part of a specific series, with optional filters.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetSeriesComics(string seriesId, ComicRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/comics", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseComicFilter(request, filter);

            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic creators whose work appears in a specific series, with optional filters.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CreatorResult GetSeriesCreators(string seriesId, CreatorRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/creators", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCreatorFilter(request, filter);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// Fetches lists of events which occur in a specific series, with optional filters.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetSeriesEvents(string seriesId, EventRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/events", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic stories from a specific series with optional filters.
        /// </summary>
        /// <param name="seriesId">Series unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetSeriesStories(string seriesId, StoryRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/stories", SeriesUrlSegment, seriesId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }
    }
}