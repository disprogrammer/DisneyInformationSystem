using Marvel.Api.Filters;
using RestSharp;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Marvel.Api
{
    /// <summary>
    /// MarvelClient partial class.
    /// </summary>
    public abstract partial class MarvelClient
    {
        /// <summary>
        /// Rest client.
        /// </summary>
        protected RestClient Client;

        /// <summary>
        /// Marvel API public key.
        /// </summary>
        protected readonly string ApiPublicKey;

        /// <summary>
        /// Marvel API private key.
        /// </summary>
        protected readonly string ApiPrivateKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarvelClient"/> class.
        /// </summary>
        /// <param name="apiPublicKey">API public key.</param>
        /// <param name="apiPrivateKey">API private key.</param>
        /// <param name="apiVersion">API version.</param>
        /// <param name="baseUrl">Base URI.</param>
        protected MarvelClient(string apiPublicKey, string apiPrivateKey, string apiVersion, string baseUrl)
        {
            ApiVersion = apiVersion;
            BaseUrl = baseUrl;
            ApiPublicKey = apiPublicKey;
            ApiPrivateKey = apiPrivateKey;

            var assembly = Assembly.GetExecutingAssembly();
            if (assembly.FullName != null)
            {
                var assemblyName = new AssemblyName(assembly.FullName);
                var version = assemblyName.Version;

                var restClientOptions = new RestClientOptions
                {
                    BaseUrl = new Uri(string.Format("{0}{1}", BaseUrl, ApiVersion)),
                    UserAgent = string.Format("marvel-csharp/{0} (.NET {1})", version, Environment.Version),
                    MaxTimeout = 30500
                };

                Client = new RestClient(restClientOptions);
            }
        }

        /// <summary>
        /// Sets request auth parameters.
        /// </summary>
        private void SetAuthInfo(RestRequest request)
        {
            var timestamp = Util.UnixTimeNow().ToString(CultureInfo.InvariantCulture);
            var hashInput = timestamp + ApiPrivateKey + ApiPublicKey;
            var hash = Util.CalculateMd5Hash(hashInput).ToLower();

            request.AddParameter("ts", timestamp);
            request.AddParameter("apikey", ApiPublicKey);
            request.AddParameter("hash", hash);
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual T? Execute<T>(RestRequest request) where T : new()
        {
            request.OnBeforeDeserialization = resp =>
            {
                if (((int)resp.StatusCode) >= 400 && resp.RawBytes != null)
                {
                    const string restException = "{{ \"RestException\" : {0} }}";
                    var content = resp.RawBytes.ToString();
                    var newJson = string.Format(restException, content);

                    resp.Content = null;
                    resp.RawBytes = Encoding.UTF8.GetBytes(newJson.ToString(CultureInfo.InvariantCulture));
                }
            };

            SetAuthInfo(request);

            var response = Client.Execute<T>(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var marvelException = new ApplicationException(message, response.ErrorException);
                throw marvelException;
            }

            return response.Data ?? default;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual RestResponse Execute(RestRequest request)
        {
            return Client.Execute(request);
        }

        /// <summary>
        /// Gets or sets BaseUrl.
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Gets or sets ApiVersion.
        /// </summary>
        public string ApiVersion { get; private set; }
    }

    /// <summary>
    /// REST API wrapper.
    /// </summary>
    public partial class MarvelRestClient : MarvelClient
    {
        /// <summary>
        /// Marvel API URI.
        /// </summary>
        private static readonly string marvelApiUri = ConfigurationManager.AppSettings["MarvelApiUrl"] ?? string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarvelRestClient"/> class.
        /// </summary>
        /// <param name="apiPublicKey">API public key</param>
        /// <param name="apiPrivateKey">API private key</param>
        public MarvelRestClient(string apiPublicKey, string apiPrivateKey)
            : base(apiPublicKey, apiPrivateKey, "v1", marvelApiUri)
        {
        }

        /// <summary>
        /// Parses the Character search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Character request filter.</param>
        private static void ParseCharacterFilter(RestRequest request, CharacterRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.Name != null) request.AddParameter("name", filter.Name);
            if (filter.NameStartsWith != null) request.AddParameter("nameStartsWith", filter.NameStartsWith);
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Comics != null) request.AddParameter("comics", filter.Comics);
            if (filter.Series != null) request.AddParameter("series", filter.Series);
            if (filter.Events != null) request.AddParameter("events", filter.Events);
            if (filter.Stories != null) request.AddParameter("stories", filter.Stories);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }

        /// <summary>
        /// Parses the Comic search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Comic request filter.</param>
        private static void ParseComicFilter(RestRequest request, ComicRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.Format.HasValue) request.AddParameter("format", filter.Format.Value.GetDescription());
            if (filter.FormatType.HasValue) request.AddParameter("formatType", filter.FormatType.Value.GetDescription());
            if (filter.NoVariants.HasValue) request.AddParameter("noVariants", filter.NoVariants.Value.ToString().ToLower());
            if (filter.DateDescriptor.HasValue) request.AddParameter("dateDescriptor", filter.DateDescriptor.Value.GetDescription());
            if (filter.DateRange != null) request.AddParameter("dateRange", filter.DateRange);
            if (filter.Title != null) request.AddParameter("title", filter.Title);
            if (filter.TitleStartsWith != null) request.AddParameter("titleStartsWith", filter.TitleStartsWith);
            if (filter.StartYear.HasValue) request.AddParameter("startYear", filter.StartYear.Value);
            if (filter.IssueNumber.HasValue) request.AddParameter("issueNumber", filter.IssueNumber.Value);
            if (filter.DiamondCode != null) request.AddParameter("diamondCode", filter.DiamondCode);
            if (filter.DigitalId.HasValue) request.AddParameter("digitalId", filter.DigitalId.Value);
            if (filter.UPC != null) request.AddParameter("upc", filter.UPC);
            if (filter.ISBN != null) request.AddParameter("isbn", filter.ISBN);
            if (filter.EAN != null) request.AddParameter("ean", filter.EAN);
            if (filter.ISSN != null) request.AddParameter("issn", filter.ISSN);
            if (filter.HasDigitalIssue.HasValue) request.AddParameter("hasDigitalIssue", filter.HasDigitalIssue.ToString() ?? "false");
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Creators != null) request.AddParameter("creators", filter.Creators);
            if (filter.Characters != null) request.AddParameter("characters", filter.Characters);
            if (filter.Series != null) request.AddParameter("series", filter.Series);
            if (filter.Events != null) request.AddParameter("events", filter.Events);
            if (filter.Stories != null) request.AddParameter("stories", filter.Stories);
            if (filter.SharedAppearances != null) request.AddParameter("sharedAppearances", filter.SharedAppearances);
            if (filter.Collaborators != null) request.AddParameter("collaborators", filter.Collaborators);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }

        /// <summary>
        /// Parses the Creator search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Creator request filter.</param>
        private static void ParseCreatorFilter(RestRequest request, CreatorRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.FirstName != null) request.AddParameter("firstName", filter.FirstName);
            if (filter.MiddleName != null) request.AddParameter("middleName", filter.MiddleName);
            if (filter.LastName != null) request.AddParameter("lastName", filter.LastName);
            if (filter.Suffix != null) request.AddParameter("suffix", filter.Suffix);
            if (filter.NameStartsWith != null) request.AddParameter("nameStartsWith", filter.NameStartsWith);
            if (filter.FirstNameStartsWith != null) request.AddParameter("firstNameStartsWith", filter.FirstNameStartsWith);
            if (filter.MiddleNameStartsWith != null) request.AddParameter("middleNameStartsWith", filter.MiddleNameStartsWith);
            if (filter.LastNameStartsWith != null) request.AddParameter("lastNameStartsWith", filter.LastNameStartsWith);
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Comics != null) request.AddParameter("comics", filter.Comics);
            if (filter.Series != null) request.AddParameter("series", filter.Series);
            if (filter.Events != null) request.AddParameter("events", filter.Events);
            if (filter.Stories != null) request.AddParameter("stories", filter.Stories);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }

        /// <summary>
        /// Parses the Event search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Event request filter.</param>
        private static void ParseEventFilter(RestRequest request, EventRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.Name != null) request.AddParameter("name", filter.Name);
            if (filter.NameStartsWith != null) request.AddParameter("nameStartsWith", filter.NameStartsWith);
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Creators != null) request.AddParameter("creators", filter.Creators);
            if (filter.Characters != null) request.AddParameter("characters", filter.Characters);
            if (filter.Series != null) request.AddParameter("series", filter.Series);
            if (filter.Comics != null) request.AddParameter("comics", filter.Comics);
            if (filter.Stories != null) request.AddParameter("stories", filter.Stories);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }

        /// <summary>
        /// Parses the Series search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Series request filter.</param>
        private static void ParseSeriesFilter(RestRequest request, SeriesRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.Title != null) request.AddParameter("title", filter.Title);
            if (filter.TitleStartsWith != null) request.AddParameter("titleStartsWith", filter.TitleStartsWith);
            if (filter.StartYear.HasValue) request.AddParameter("startYear", filter.StartYear.Value);
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Comics != null) request.AddParameter("comics", filter.Comics);
            if (filter.Stories != null) request.AddParameter("stories", filter.Stories);
            if (filter.Events != null) request.AddParameter("events", filter.Events);
            if (filter.Creators != null) request.AddParameter("creators", filter.Creators);
            if (filter.Characters != null) request.AddParameter("characters", filter.Characters);
            if (filter.SeriesType.HasValue) request.AddParameter("seriesType", filter.SeriesType.HasValue.GetDescription());
            if (filter.Contains != null) request.AddParameter("contains", filter.Contains);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }

        /// <summary>
        /// Parses the Story search.
        /// </summary>
        /// <param name="request">Rest request.</param>
        /// <param name="filter">Story request filter.</param>
        private static void ParseStoryFilter(RestRequest request, StoryRequestFilter filter)
        {
            if (filter == null) return;
            if (filter.ModifiedSince.HasValue) request.AddParameter("modifiedSince", filter.ModifiedSince.Value.ToString("yyyy-MM-dd"));
            if (filter.Comics != null) request.AddParameter("comics", filter.Comics);
            if (filter.Series != null) request.AddParameter("series", filter.Series);
            if (filter.Events != null) request.AddParameter("events", filter.Events);
            if (filter.Creators != null) request.AddParameter("creators", filter.Creators);
            if (filter.Characters != null) request.AddParameter("characters", filter.Characters);
            if (filter.ResultSetOrder != null) request.AddParameter("orderBy", filter.ResultSetOrder);
            if (filter.Limit.HasValue) request.AddParameter("limit", filter.Limit.Value);
            if (filter.Offset.HasValue) request.AddParameter("offset", filter.Offset.Value);
        }
    }
}