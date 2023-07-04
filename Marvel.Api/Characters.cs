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
        /// Character URI.
        /// </summary>
        private readonly string? CharactersUrlSegment = ConfigurationManager.AppSettings["CharacterUrl"];

        /// <summary>
        /// Fetches lists of comic characters with optional filters.
        /// </summary>
        /// <param name="filter">Search query filter data</param>
        public virtual CharacterResult GetCharacters(CharacterRequestFilter filter)
        {
            var request = new RestRequest(CharactersUrlSegment, Method.Get);
            ParseCharacterFilter(request, filter);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// This method fetches a single character resource.
        /// It is the canonical URI for any character resource provided by the API.
        /// </summary>
        /// <param name="characterId">Character unique identifier</param>
        public virtual CharacterResult GetCharacter(string characterId)
        {
            var requestUrl = string.Format("{0}/{1}", CharactersUrlSegment, characterId);
            var request = new RestRequest(requestUrl, Method.Get);
            return Execute<CharacterResult>(request);
        }

        /// <summary>
        /// Fetches lists of comics containing a specific character, with optional filters.
        /// </summary>
        /// <param name="characterId">Character unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual ComicResult GetCharacterComics(string characterId, ComicRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/comics", CharactersUrlSegment, characterId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseComicFilter(request, filter);
            return Execute<ComicResult>(request);
        }

        /// <summary>
        /// Fetches lists of events in which a specific character appears, with optional filters.
        /// </summary>
        /// <param name="characterId">Character unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual EventResult GetCharacterEvents(string characterId, EventRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/events", CharactersUrlSegment, characterId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseEventFilter(request, filter);
            return Execute<EventResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic series in which a specific character appears, with optional filters.
        /// </summary>
        /// <param name="characterId">Character unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual SeriesResult GetCharacterSeries(string characterId, SeriesRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/series", CharactersUrlSegment, characterId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseSeriesFilter(request, filter);
            return Execute<SeriesResult>(request);
        }

        /// <summary>
        /// Fetches lists of comic stories featuring a specific character with optional filters.
        /// </summary>
        /// <param name="characterId">Character unique identifier</param>
        /// <param name="filter">Search query filter data</param>
        public virtual StoryResult GetCharacterStories(string characterId, StoryRequestFilter filter)
        {
            var requestUrl = string.Format("{0}/{1}/stories", CharactersUrlSegment, characterId);
            var request = new RestRequest(requestUrl, Method.Get);
            ParseStoryFilter(request, filter);
            return Execute<StoryResult>(request);
        }
    }
}