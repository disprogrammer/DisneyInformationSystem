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
        /// Creators URI.
        /// </summary>
        private readonly string CreatorsUrlSegment = ConfigurationManager.AppSettings["CreatorsUrl"];

        /// <summary>
        /// Fetches lists of comic creators with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual CreatorResult GetCreators(CreatorRequestFilter filter)
        {
            var request = new RestRequest(CreatorsUrlSegment, Method.Get);
            ParseCreatorFilter(request, filter);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// This method fetches a single creator resource.
        /// It is the canonical URI for any creator resource provided by the API.
        /// </summary>
        /// <param name="creatorId">Creator unique identifier</param>
        public virtual CreatorResult GetCreator(string creatorId)
        {
            var requestUrl = string.Format("{0}/{1}", CreatorsUrlSegment, creatorId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// Fetches lists of comics in which the work of a specific creator appears, with optional filters.
        /// </summary>
        /// <param name="creatorId">Creator unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetCreatorComics(string creatorId, ComicRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/comics", CreatorsUrlSegment, creatorId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseComicFilter(request, filter);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of events featuring the work of a specific creator with optional filters.
        /// </summary>
        /// <param name="creatorId">Creator unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetCreatorEvents(string creatorId, EventRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/events", CreatorsUrlSegment, creatorId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic series in which a specific creator's work appears, with optional filters.
        /// </summary>
        /// <param name="creatorId">Creator unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual SeriesResult GetCreatorSeries(string creatorId, SeriesRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/series", CreatorsUrlSegment, creatorId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseSeriesFilter(request, filter);
            return Execute<SeriesResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic stories by a specific creator with optional filters.
        /// </summary>
        /// <param name="creatorId">Creator unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetCreatorStories(string creatorId, StoryRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/stories", CreatorsUrlSegment, creatorId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }
    }
}