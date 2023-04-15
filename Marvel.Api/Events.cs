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
        /// Events URI.
        /// </summary>
        private readonly string EventsUrlSegment = ConfigurationManager.AppSettings["EventsUrl"];

        /// <summary>
        /// Fetches lists of events with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetEvents(EventRequestFilter filter)
        {
            var request = new RestRequest(EventsUrlSegment, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// This method fetches a single event resource.
        /// It is the canonical URI for any event resource provided by the API.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        public virtual EventResult GetEvent(string eventId)
        {
            var requestUrl = string.Format("{0}/{1}", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of characters which appear in a specific event, with optional filters.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CharacterResult GetEventCharacters(string eventId, CharacterRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/characters", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCharacterFilter(request, filter);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// Fetches lists of comics which take place during a specific event, with optional filters.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetEventComics(string eventId, ComicRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/comics", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseComicFilter(request, filter);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic creators whose work appears in a specific event,
        /// with optional filters.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual CreatorResult GetEventCreators(string eventId, CreatorRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/creators", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseCreatorFilter(request, filter);
            return Execute<CreatorResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic series in which a specific event takes place, with optional filters.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual SeriesResult GetEventSeries(string eventId, SeriesRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/series", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseSeriesFilter(request, filter);
            return Execute<SeriesResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic stories from a specific event, with optional filters.
        /// </summary>
        /// <param name="eventId">Event unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetEventStories(string eventId, StoryRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/stories", EventsUrlSegment, eventId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }
    }
}