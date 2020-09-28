// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
#if DOTNETCORE
using System.Net.Http;
using System.Runtime.InteropServices;
#endif
using System.Net.Security;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Allows you to control how <see cref="ElasticLowLevelClient"/> behaves and where/how it connects to Elasticsearch
	/// </summary>
	public class ConnectionConfiguration : ConnectionConfiguration<ConnectionConfiguration>
	{
		/// <summary>
		/// Detects whether we are running on .NET Core with CurlHandler.
		/// If this is true, we will set a very restrictive <see cref="DefaultConnectionLimit"/>
		/// As the old curl based handler is known to bleed TCP connections:
		/// <para>https://github.com/dotnet/runtime/issues/22366</para>
		/// </summary>
        private static bool UsingCurlHandler
		{
			get
			{
#if !DOTNETCORE
				return false;
#else
				var curlHandlerExists = typeof(HttpClientHandler).Assembly.GetType("System.Net.Http.CurlHandler") != null;
				if (!curlHandlerExists) return false;

				var socketsHandlerExists = typeof(HttpClientHandler).Assembly.GetType("System.Net.Http.SocketsHttpHandler") != null;
				// running on a .NET core version with CurlHandler, before the existence of SocketsHttpHandler.
				// Must be using CurlHandler.
				if (!socketsHandlerExists) return true;

				if (AppContext.TryGetSwitch("System.Net.Http.UseSocketsHttpHandler", out var isEnabled))
					return !isEnabled;

				var environmentVariable =
					Environment.GetEnvironmentVariable("DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER");

				// SocketsHandler exists and no environment variable exists to disable it.
				// Must be using SocketsHandler and not CurlHandler
				if (environmentVariable == null) return false;

				return environmentVariable.Equals("false", StringComparison.OrdinalIgnoreCase) ||
					environmentVariable.Equals("0");
#endif
			}
		}

		/// <summary>
		/// The default ping timeout. Defaults to 2 seconds
		/// </summary>
		public static readonly TimeSpan DefaultPingTimeout = TimeSpan.FromSeconds(2);

		/// <summary>
		/// The default ping timeout when the connection is over HTTPS. Defaults to
		/// 5 seconds
		/// </summary>
		public static readonly TimeSpan DefaultPingTimeoutOnSSL = TimeSpan.FromSeconds(5);

		/// <summary>
		/// The default timeout before the client aborts a request to Elasticsearch.
		/// Defaults to 1 minute
		/// </summary>
		public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

		/// <summary>
		/// The default timeout before a TCP connection is forcefully recycled so that DNS updates come through
		/// Defaults to 5 minutes.
		/// </summary>
		public static readonly TimeSpan DefaultDnsRefreshTimeout = TimeSpan.FromMinutes(5);

		/// <summary>
		/// The default connection limit for both Elasticsearch.Net and Nest. Defaults to <c>80</c>
#if DOTNETCORE
		/// <para>Except for <see cref="HttpClientHandler"/> implementations based on curl, which defaults to <see cref="Environment.ProcessorCount"/></para>
#endif
		/// </summary>
		public static readonly int DefaultConnectionLimit = UsingCurlHandler ? Environment.ProcessorCount : 80;

		/// <summary>
		/// The default user agent for Elasticsearch.Net
		/// </summary>
		public static readonly string DefaultUserAgent = $"elasticsearch-net/{typeof(IConnectionConfigurationValues).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion} ({RuntimeInformation.OSDescription}; {RuntimeInformation.FrameworkDescription}; Elasticsearch.Net)";

		/// <summary>
		/// Creates a new instance of <see cref="ConnectionConfiguration"/>
		/// </summary>
		/// <param name="uri">The root of the Elasticsearch node we want to connect to. Defaults to http://localhost:9200</param>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public ConnectionConfiguration(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200"))) { }

		/// <summary>
		/// Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId"/>,
		/// <para><see cref="CloudConnectionPool"/> documentation for more information on how to obtain your Cloud Id</para>
		/// </summary>
		public ConnectionConfiguration(string cloudId, BasicAuthenticationCredentials credentials) : this(new CloudConnectionPool(cloudId, credentials)) { }

		/// <summary>
		/// Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId"/>,
		/// <para><see cref="CloudConnectionPool"/> documentation for more information on how to obtain your Cloud Id</para>
		/// </summary>
		public ConnectionConfiguration(string cloudId, ApiKeyAuthenticationCredentials credentials) : this(new CloudConnectionPool(cloudId, credentials)) { }

		/// <summary>
		/// Creates a new instance of <see cref="ConnectionConfiguration"/>
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that tells the client what nodes are available</param>
		public ConnectionConfiguration(IConnectionPool connectionPool)
			// ReSharper disable once IntroduceOptionalParameters.Global
			: this(connectionPool, null, null) { }

		/// <summary>
		/// Creates a new instance of <see cref="ConnectionConfiguration"/>
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that tells the client what nodes are available</param>
		/// <param name="connection">An connection implementation that can make API requests</param>
		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection)
			// ReSharper disable once IntroduceOptionalParameters.Global
			: this(connectionPool, connection, null) { }

		/// <summary>
		/// Creates a new instance of <see cref="ConnectionConfiguration"/>
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that tells the client what nodes are available</param>
		/// <param name="serializer">A serializer implementation used to serialize requests and deserialize responses</param>
		public ConnectionConfiguration(IConnectionPool connectionPool, IElasticsearchSerializer serializer)
			: this(connectionPool, null, serializer) { }

		/// <summary>
		/// Creates a new instance of <see cref="ConnectionConfiguration"/>
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that tells the client what nodes are available</param>
		/// <param name="connection">An connection implementation that can make API requests</param>
		/// <param name="serializer">A serializer implementation used to serialize requests and deserialize responses</param>
		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection, IElasticsearchSerializer serializer)
			: base(connectionPool, connection, serializer) { }

	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionConfiguration<T> : IConnectionConfigurationValues
		where T : ConnectionConfiguration<T>
	{
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly NameValueCollection _headers = new NameValueCollection();
		private readonly NameValueCollection _queryString = new NameValueCollection();
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		private readonly ElasticsearchUrlFormatter _urlFormatter;

		private BasicAuthenticationCredentials _basicAuthCredentials;
		private ApiKeyAuthenticationCredentials _apiKeyAuthCredentials;
		private X509CertificateCollection _clientCertificates;
		private Action<IApiCallDetails> _completedRequestHandler = DefaultCompletedRequestHandler;
		private int _connectionLimit;
		private TimeSpan? _deadTimeout;
		private bool _disableAutomaticProxyDetection;
		private bool _disableDirectStreaming;
		private bool _disablePings;
		private bool _enableHttpCompression;
		private bool _enableHttpPipelining = true;
		private TimeSpan? _keepAliveInterval;
		private TimeSpan? _keepAliveTime;
		private TimeSpan? _maxDeadTimeout;
		private int? _maxRetries;
		private TimeSpan? _maxRetryTimeout;
		private Func<Node, bool> _nodePredicate = DefaultNodePredicate;
		private Action<RequestData> _onRequestDataCreated = DefaultRequestDataCreated;
		private TimeSpan? _pingTimeout;
		private bool _prettyJson;
		private string _proxyAddress;
		private SecureString _proxyPassword;
		private string _proxyUsername;
		private TimeSpan _requestTimeout;
		private TimeSpan _dnsRefreshTimeout;
		private Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> _serverCertificateValidationCallback;
		private IReadOnlyCollection<int> _skipDeserializationForStatusCodes = new ReadOnlyCollection<int>(new int[] { });
		private TimeSpan? _sniffLifeSpan;
		private bool _sniffOnConnectionFault;
		private bool _sniffOnStartup;
		private bool _throwExceptions;
		private bool _transferEncodingChunked;
		private IMemoryStreamFactory _memoryStreamFactory = DefaultMemoryStreamFactory;
		private bool _enableTcpStats;
		//public static IMemoryStreamFactory Default { get; } = RecyclableMemoryStreamFactory.Default;
		public static IMemoryStreamFactory DefaultMemoryStreamFactory { get; } = Elasticsearch.Net.MemoryStreamFactory.Default;
		private bool _enableThreadPoolStats;

		private string _userAgent = ConnectionConfiguration.DefaultUserAgent;
		private Func<HttpMethod, int, bool> _statusCodeToResponseSuccess;

		protected ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection, IElasticsearchSerializer requestResponseSerializer)
		{
			_connectionPool = connectionPool;
			_connection = connection ?? new HttpConnection();
			var serializer = requestResponseSerializer ?? new SystemTextJsonSerializer();
			UseThisRequestResponseSerializer = new DiagnosticsSerializerProxy(serializer);

			_connectionLimit = ConnectionConfiguration.DefaultConnectionLimit;
			_requestTimeout = ConnectionConfiguration.DefaultTimeout;
			_dnsRefreshTimeout = ConnectionConfiguration.DefaultDnsRefreshTimeout;
			_sniffOnConnectionFault = true;
			_sniffOnStartup = true;
			_sniffLifeSpan = TimeSpan.FromHours(1);
			if (_connectionPool.SupportsReseeding)
				_nodePredicate = DefaultReseedableNodePredicate;

			_urlFormatter = new ElasticsearchUrlFormatter(this);
			_statusCodeToResponseSuccess = (m, i) => HttpStatusCodeClassifier(m, i);

			if (connectionPool is CloudConnectionPool cloudPool)
			{
				_basicAuthCredentials = cloudPool.BasicCredentials;
				_apiKeyAuthCredentials = cloudPool.ApiKeyCredentials;
				_enableHttpCompression = true;
			}

		}

		protected IElasticsearchSerializer UseThisRequestResponseSerializer { get; set; }
		BasicAuthenticationCredentials IConnectionConfigurationValues.BasicAuthenticationCredentials => _basicAuthCredentials;
		ApiKeyAuthenticationCredentials IConnectionConfigurationValues.ApiKeyAuthenticationCredentials => _apiKeyAuthCredentials;
		SemaphoreSlim IConnectionConfigurationValues.BootstrapLock => _semaphore;
		X509CertificateCollection IConnectionConfigurationValues.ClientCertificates => _clientCertificates;
		IConnection IConnectionConfigurationValues.Connection => _connection;
		int IConnectionConfigurationValues.ConnectionLimit => _connectionLimit;
		IConnectionPool IConnectionConfigurationValues.ConnectionPool => _connectionPool;
		TimeSpan? IConnectionConfigurationValues.DeadTimeout => _deadTimeout;
		bool IConnectionConfigurationValues.DisableAutomaticProxyDetection => _disableAutomaticProxyDetection;
		bool IConnectionConfigurationValues.DisableDirectStreaming => _disableDirectStreaming;
		bool IConnectionConfigurationValues.DisablePings => _disablePings;
		bool IConnectionConfigurationValues.EnableHttpCompression => _enableHttpCompression;
		NameValueCollection IConnectionConfigurationValues.Headers => _headers;
		bool IConnectionConfigurationValues.HttpPipeliningEnabled => _enableHttpPipelining;
		TimeSpan? IConnectionConfigurationValues.KeepAliveInterval => _keepAliveInterval;
		TimeSpan? IConnectionConfigurationValues.KeepAliveTime => _keepAliveTime;
		TimeSpan? IConnectionConfigurationValues.MaxDeadTimeout => _maxDeadTimeout;
		int? IConnectionConfigurationValues.MaxRetries => _maxRetries;
		TimeSpan? IConnectionConfigurationValues.MaxRetryTimeout => _maxRetryTimeout;
		IMemoryStreamFactory IConnectionConfigurationValues.MemoryStreamFactory => _memoryStreamFactory;

		Func<Node, bool> IConnectionConfigurationValues.NodePredicate => _nodePredicate;
		Action<IApiCallDetails> IConnectionConfigurationValues.OnRequestCompleted => _completedRequestHandler;
		Action<RequestData> IConnectionConfigurationValues.OnRequestDataCreated => _onRequestDataCreated;
		TimeSpan? IConnectionConfigurationValues.PingTimeout => _pingTimeout;
		bool IConnectionConfigurationValues.PrettyJson => _prettyJson;
		string IConnectionConfigurationValues.ProxyAddress => _proxyAddress;
		SecureString IConnectionConfigurationValues.ProxyPassword => _proxyPassword;
		string IConnectionConfigurationValues.ProxyUsername => _proxyUsername;
		NameValueCollection IConnectionConfigurationValues.QueryStringParameters => _queryString;
		IElasticsearchSerializer IConnectionConfigurationValues.RequestResponseSerializer => UseThisRequestResponseSerializer;
		TimeSpan IConnectionConfigurationValues.RequestTimeout => _requestTimeout;
		TimeSpan IConnectionConfigurationValues.DnsRefreshTimeout => _dnsRefreshTimeout;

		Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> IConnectionConfigurationValues.ServerCertificateValidationCallback =>
			_serverCertificateValidationCallback;

		IReadOnlyCollection<int> IConnectionConfigurationValues.SkipDeserializationForStatusCodes => _skipDeserializationForStatusCodes;
		TimeSpan? IConnectionConfigurationValues.SniffInformationLifeSpan => _sniffLifeSpan;
		bool IConnectionConfigurationValues.SniffsOnConnectionFault => _sniffOnConnectionFault;
		bool IConnectionConfigurationValues.SniffsOnStartup => _sniffOnStartup;
		bool IConnectionConfigurationValues.ThrowExceptions => _throwExceptions;
		ElasticsearchUrlFormatter IConnectionConfigurationValues.UrlFormatter => _urlFormatter;
		string IConnectionConfigurationValues.UserAgent => _userAgent;
		Func<HttpMethod, int, bool> IConnectionConfigurationValues.StatusCodeToResponseSuccess => _statusCodeToResponseSuccess;
		bool IConnectionConfigurationValues.TransferEncodingChunked => _transferEncodingChunked;
		bool IConnectionConfigurationValues.EnableTcpStats => _enableTcpStats;
		bool IConnectionConfigurationValues.EnableThreadPoolStats => _enableThreadPoolStats;

		void IDisposable.Dispose() => DisposeManagedResources();

		private static void DefaultCompletedRequestHandler(IApiCallDetails response) { }

		private static void DefaultRequestDataCreated(RequestData response) { }

		/// <summary>
		/// The default predicate for <see cref="IConnectionPool" /> implementations that return true for
		/// <see cref="IConnectionPool.SupportsReseeding" />
		/// in which case master only nodes are excluded from API calls.
		/// </summary>
		private static bool DefaultReseedableNodePredicate(Node node) => !node.MasterOnlyNode;

		private static bool DefaultNodePredicate(Node node) => true;

		protected T Assign<TValue>(TValue value, Action<T, TValue> assigner) => Fluent.Assign((T)this, value, assigner);

		/// <summary>
		/// Sets the keep-alive option on a TCP connection.
		/// <para>For Desktop CLR, sets ServicePointManager.SetTcpKeepAlive</para>
		/// </summary>
		/// <param name="keepAliveTime">Specifies the timeout with no activity until the first keep-alive packet is sent.</param>
		/// <param name="keepAliveInterval">
		/// Specifies the interval between when successive keep-alive packets are sent if no acknowledgement is
		/// received.
		/// </param>
		public T EnableTcpKeepAlive(TimeSpan keepAliveTime, TimeSpan keepAliveInterval) =>
			Assign(keepAliveTime, (a, v) => a._keepAliveTime = v)
			.Assign(keepAliveInterval, (a, v) => a._keepAliveInterval = v);

		/// <summary> The maximum number of retries for a given request </summary>
		public T MaximumRetries(int maxRetries) => Assign(maxRetries, (a, v) => a._maxRetries = v);

		/// <summary>
		/// Limits the number of concurrent connections that can be opened to an endpoint. Defaults to <c>80</c> for all IConnection
		/// implementations that are not based on <c>System.Net.Http.CurlHandler</c>. For those based on System.Net.Http.CurlHandler, defaults
		/// to <c>Environment.ProcessorCount</c>.
		/// <para>
		/// For Desktop CLR, this setting applies to the DefaultConnectionLimit property on the  ServicePointManager object when creating
		/// ServicePoint objects, affecting the default <see cref="IConnection" /> implementation.
		/// </para>
		/// <para>
		/// For Core CLR, this setting applies to the MaxConnectionsPerServer property on the HttpClientHandler instances used by the HttpClient
		/// inside the default <see cref="IConnection" /> implementation
		/// </para>
		/// </summary>
		/// <param name="connectionLimit">The connection limit, a value lower then 0 will cause the connection limit not to be set at all</param>
		public T ConnectionLimit(int connectionLimit) => Assign(connectionLimit, (a, v) => a._connectionLimit = v);

		/// <summary>
		/// Enables resniffing of the cluster when a call fails, if the connection pool supports reseeding. Defaults to <c>true</c>
		/// </summary>
		public T SniffOnConnectionFault(bool sniffsOnConnectionFault = true) =>
			Assign(sniffsOnConnectionFault, (a, v) => a._sniffOnConnectionFault = v);

		/// <summary>
		/// Enables sniffing on first usage of a connection pool if that pool supports reseeding. Defaults to <c>true</c>
		/// </summary>
		public T SniffOnStartup(bool sniffsOnStartup = true) => Assign(sniffsOnStartup, (a, v) => a._sniffOnStartup = v);

		/// <summary>
		/// Set the duration after which a cluster state is considered stale and a sniff should be performed again.
		/// An <see cref="IConnectionPool" /> has to signal it supports reseeding, otherwise sniffing will never happen.
		/// Defaults to 1 hour.
		/// Set to null to disable completely. Sniffing will only ever happen on ConnectionPools that return true for SupportsReseeding
		/// </summary>
		/// <param name="sniffLifeSpan">The duration a clusterstate is considered fresh, set to null to disable periodic sniffing</param>
		public T SniffLifeSpan(TimeSpan? sniffLifeSpan) => Assign(sniffLifeSpan, (a, v) => a._sniffLifeSpan = v);

		/// <summary>
		/// Enables gzip compressed requests and responses.
		/// <para>IMPORTANT: You need to configure http compression on Elasticsearch to be able to use this</para>
		/// <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/modules-http.html</para>
		/// </summary>
		public T EnableHttpCompression(bool enabled = true) => Assign(enabled, (a, v) => a._enableHttpCompression = v);

		/// <summary>
		/// Disables the automatic detection of a proxy
		/// </summary>
		public T DisableAutomaticProxyDetection(bool disable = true) => Assign(disable, (a, v) => a._disableAutomaticProxyDetection = v);

		/// <summary>
		/// Instead of following a c/go like error checking on response.IsValid do throw an exception (except when <see cref="IApiCallDetails.SuccessOrKnownError"/> is false)
		/// on the client when a call resulted in an exception on either the client or the Elasticsearch server.
		/// <para>Reasons for such exceptions could be search parser errors, index missing exceptions, etc...</para>
		/// </summary>
		public T ThrowExceptions(bool alwaysThrow = true) => Assign(alwaysThrow, (a, v) => a._throwExceptions = v);

		/// <summary>
		/// When a node is used for the very first time or when it's used for the first time after it has been marked dead
		/// a ping with a very low timeout is send to the node to make sure that when it's still dead it reports it as fast as possible.
		/// You can disable these pings globally here if you rather have it fail on the possible slower original request
		/// </summary>
		public T DisablePing(bool disable = true) => Assign(disable, (a, v) => a._disablePings = v);

		/// <summary>
		/// A collection of query string parameters that will be sent with every request. Useful in situations where you always need to pass a
		/// parameter e.g. an API key.
		/// </summary>
		public T GlobalQueryStringParameters(NameValueCollection queryStringParameters) => Assign(queryStringParameters, (a, v) => a._queryString.Add(v));

		/// <summary>
		/// A collection of headers that will be sent with every request. Useful in situations where you always need to pass a header e.g. a custom
		/// auth header
		/// </summary>
		public T GlobalHeaders(NameValueCollection headers) => Assign(headers, (a, v) => a._headers.Add(v));

		/// <summary>
		/// Sets the default timeout in milliseconds for each request to Elasticsearch. Defaults to <c>60</c> seconds.
		/// <para>NOTE: You can set this to a high value here, and specify a timeout on Elasticsearch's side.</para>
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		public T RequestTimeout(TimeSpan timeout) => Assign(timeout, (a, v) => a._requestTimeout = v);

		/// <summary>
		/// Sets the default ping timeout in milliseconds for ping requests, which are used
		/// to determine whether a node is alive. Pings should fail as fast as possible.
		/// </summary>
		/// <param name="timeout">The ping timeout in milliseconds defaults to <c>1000</c>, or <c>2000</c> if using SSL.</param>
		public T PingTimeout(TimeSpan timeout) => Assign(timeout, (a, v) => a._pingTimeout = v);

		/// <summary>
		/// Sets the default dead timeout factor when a node has been marked dead.
		/// </summary>
		/// <remarks>Some connection pools may use a flat timeout whilst others take this factor and increase it exponentially</remarks>
		/// <param name="timeout"></param>
		public T DeadTimeout(TimeSpan timeout) => Assign(timeout, (a, v) => a._deadTimeout = v);

		/// <summary>
		/// Sets the maximum time a node can be marked dead.
		/// Different implementations of <see cref="IConnectionPool" /> may choose a different default.
		/// </summary>
		/// <param name="timeout">The timeout in milliseconds</param>
		public T MaxDeadTimeout(TimeSpan timeout) => Assign(timeout, (a, v) => a._maxDeadTimeout = v);

		/// <summary>
		/// Limits the total runtime, including retries, separately from <see cref="RequestTimeout" />
		/// <para>
		/// When not specified, defaults to <see cref="RequestTimeout" />, which itself defaults to <c>60</c> seconds
		/// </para>
		/// </summary>
		public T MaxRetryTimeout(TimeSpan maxRetryTimeout) => Assign(maxRetryTimeout, (a, v) => a._maxRetryTimeout = v);

		/// <summary>
		/// DnsRefreshTimeout for the connections. Defaults to 5 minutes.
		#if DOTNETCORE
		/// <para>Will create new instances of <see cref="System.Net.Http.HttpClient"/> after this timeout to force DNS updates</para>
		#else
		/// <para>Will set both <see cref="System.Net.ServicePointManager.DnsRefreshTimeout"/> and <see cref="System.Net.ServicePointManager.ConnectionLeaseTimeout "/>
		#endif
		/// </summary>
		public T DnsRefreshTimeout(TimeSpan timeout) => Assign(timeout, (a, v) => a._dnsRefreshTimeout = v);

		/// <summary>
		/// If your connection has to go through proxy, use this method to specify the proxy url
		/// </summary>
		public T Proxy(Uri proxyAddress, string username, string password) =>
			Assign(proxyAddress.ToString(), (a, v) => a._proxyAddress = v)
				.Assign(username, (a, v) => a._proxyUsername = v)
				.Assign(password, (a, v) => a._proxyPassword = v.CreateSecureString());

		/// <summary>
		/// If your connection has to go through proxy, use this method to specify the proxy url
		/// </summary>
		public T Proxy(Uri proxyAddress, string username, SecureString password) =>
			Assign(proxyAddress.ToString(), (a, v) => a._proxyAddress = v)
				.Assign(username, (a, v) => a._proxyUsername = v)
				.Assign(password, (a, v) => a._proxyPassword = v);

		/// <summary>
		/// Forces all requests to have ?pretty=true querystring parameter appended,
		/// causing Elasticsearch to return formatted JSON.
		/// Defaults to <c>false</c>
		/// </summary>
		public T PrettyJson(bool b = true) => Assign(b, (a, v) =>
		{
			a._prettyJson = v;
			const string key = "pretty";
			if (!v && a._queryString[key] != null) a._queryString.Remove(key);
			else if (v && a._queryString[key] == null)
				a.GlobalQueryStringParameters(new NameValueCollection { { key, "true" } });
		});

		/// <summary>
		/// Forces all requests to have ?error_trace=true querystring parameter appended,
		/// causing Elasticsearch to return stack traces as part of serialized exceptions
		/// Defaults to <c>false</c>
		/// </summary>
		public T IncludeServerStackTraceOnError(bool b = true) => Assign(b, (a, v) =>
		{
			const string key = "error_trace";
			if (!v && a._queryString[key] != null) a._queryString.Remove(key);
			else if (v && a._queryString[key] == null)
				a.GlobalQueryStringParameters(new NameValueCollection { { key, "true" } });
		});

		/// <summary>
		/// Ensures the response bytes are always available on the <see cref="ElasticsearchResponse{T}" />
		/// <para>
		/// IMPORTANT: Depending on the registered serializer,
		/// this may cause the response to be buffered in memory first, potentially affecting performance.
		/// </para>
		/// </summary>
		public T DisableDirectStreaming(bool b = true) => Assign(b, (a, v) => a._disableDirectStreaming = v);

		/// <summary>
		/// Registers an <see cref="Action{IApiCallDetails}" /> that is called when a response is received from Elasticsearch.
		/// This can be useful for implementing custom logging.
		/// Multiple callbacks can be registered by calling this multiple times
		/// </summary>
		public T OnRequestCompleted(Action<IApiCallDetails> handler) =>
			Assign(handler, (a, v) => a._completedRequestHandler += v ?? DefaultCompletedRequestHandler);

		/// <summary>
		/// Registers an <see cref="Action{RequestData}" /> that is called when <see cref="RequestData" /> is created.
		/// Multiple callbacks can be registered by calling this multiple times
		/// </summary>
		public T OnRequestDataCreated(Action<RequestData> handler) =>
			Assign(handler, (a, v) => a._onRequestDataCreated += v ?? DefaultRequestDataCreated);

		/// <summary>
		/// Basic Authentication credentials to send with all requests to Elasticsearch
		/// </summary>
		public T BasicAuthentication(string username, string password) =>
			Assign(new BasicAuthenticationCredentials(username, password), (a, v) => a._basicAuthCredentials = v);

		/// <summary>
		/// Basic Authentication credentials to send with all requests to Elasticsearch
		/// </summary>
		public T BasicAuthentication(string username, SecureString password) =>
			Assign(new BasicAuthenticationCredentials(username, password), (a, v) => a._basicAuthCredentials = v);

		/// <summary>
		/// Api Key to send with all requests to Elasticsearch
		/// </summary>
		public T ApiKeyAuthentication(string id, SecureString apiKey) =>
			Assign(new ApiKeyAuthenticationCredentials(id, apiKey), (a, v) => a._apiKeyAuthCredentials = v);

		/// <summary>
		/// Api Key to send with all requests to Elasticsearch
		/// </summary>
		public T ApiKeyAuthentication(string id, string apiKey) =>
			Assign(new ApiKeyAuthenticationCredentials(id, apiKey), (a, v) => a._apiKeyAuthCredentials = v);

		/// <summary>
		/// Api Key to send with all requests to Elasticsearch
		/// </summary>
		public T ApiKeyAuthentication(ApiKeyAuthenticationCredentials credentials) =>
			Assign(credentials, (a, v) => a._apiKeyAuthCredentials = v);

		/// <summary>
		/// Allows for requests to be pipelined. http://en.wikipedia.org/wiki/HTTP_pipelining
		/// <para>NOTE: HTTP pipelining must also be enabled in Elasticsearch for this to work properly.</para>
		/// </summary>
		public T EnableHttpPipelining(bool enabled = true) => Assign(enabled, (a, v) => a._enableHttpPipelining = v);

		/// <summary>
		/// Register a predicate to select which nodes that you want to execute API calls on. Note that sniffing requests omit this predicate and
		/// always execute on all nodes.
		/// When using an <see cref="IConnectionPool" /> implementation that supports reseeding of nodes, this will default to omitting master only
		/// node from regular API calls.
		/// When using static or single node connection pooling it is assumed the list of node you instantiate the client with should be taken
		/// verbatim.
		/// </summary>
		/// <param name="predicate">Return true if you want the node to be used for API calls</param>
		public T NodePredicate(Func<Node, bool> predicate) => Assign(predicate ?? DefaultNodePredicate, (a, v) => a._nodePredicate = v);

		/// <summary>
		/// Turns on settings that aid in debugging like DisableDirectStreaming() and PrettyJson()
		/// so that the original request and response JSON can be inspected. It also always asks the server for the full stack trace on errors
		/// </summary>
		/// <param name="onRequestCompleted">
		/// An optional callback to be performed when the request completes. This will
		/// not overwrite the global OnRequestCompleted callback that is set directly on
		/// ConnectionSettings. If no callback is passed, DebugInformation from the response
		/// will be written to the debug output by default.
		/// </param>
		public T EnableDebugMode(Action<IApiCallDetails> onRequestCompleted = null) =>
			PrettyJson()
				.IncludeServerStackTraceOnError()
				.DisableDirectStreaming()
				.EnableTcpStats()
				.EnableThreadPoolStats()
				.Assign(onRequestCompleted, (a, v) =>
					_completedRequestHandler += v ?? (d => Debug.WriteLine(d.DebugInformation)));

		/// <summary>
		/// Register a ServerCertificateValidationCallback, this is called per endpoint until it returns true.
		/// After this callback returns true that endpoint is validated for the lifetime of the ServiceEndpoint
		/// for that host.
		/// </summary>
		public T ServerCertificateValidationCallback(Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> callback) =>
			Assign(callback, (a, v) => a._serverCertificateValidationCallback = v);

		/// <summary>
		/// Use the following certificates to authenticate all HTTP requests. You can also set them on individual
		/// request using <see cref="RequestConfiguration.ClientCertificates" />
		/// </summary>
		public T ClientCertificates(X509CertificateCollection certificates) =>
			Assign(certificates, (a, v) => a._clientCertificates = v);

		/// <summary>
		/// Use a <see cref="System.Security.Cryptography.X509Certificates.X509Certificate"/> to authenticate all HTTP requests. You can also set them on individual request using <see cref="RequestConfiguration.ClientCertificates" />
		/// </summary>
		public T ClientCertificate(X509Certificate certificate) =>
			Assign(new X509Certificate2Collection { certificate }, (a, v) => a._clientCertificates = v);

		/// <summary>
		/// Use a file path to a certificate to authenticate all HTTP requests. You can also set them on individual request using <see cref="RequestConfiguration.ClientCertificates" />
		/// </summary>
		public T ClientCertificate(string certificatePath) =>
			Assign(new X509Certificate2Collection { new X509Certificate(certificatePath) }, (a, v) => a._clientCertificates = v);

		/// <summary>
		/// Configure the client to skip deserialization of certain status codes e.g: you run Elasticsearch behind a proxy that returns a HTML for 401,
		/// 500
		/// </summary>
		public T SkipDeserializationForStatusCodes(params int[] statusCodes) =>
			Assign(new ReadOnlyCollection<int>(statusCodes), (a, v) => a._skipDeserializationForStatusCodes = v);

		/// <summary>
		/// The user agent string to send with requests. Useful for debugging purposes to understand client and framework
		/// versions that initiate requests to Elasticsearch
		/// </summary>
		public T UserAgent(string userAgent) => Assign(userAgent, (a, v) => a._userAgent = v);

		/// <summary>
		/// Whether the request should be sent with chunked Transfer-Encoding. Default is <c>false</c>
		/// </summary>
		public T TransferEncodingChunked(bool transferEncodingChunked = true) => Assign(transferEncodingChunked, (a, v) => a._transferEncodingChunked = v);

		/// <summary>
		/// The memory stream factory to use, defaults to <see cref="RecyclableMemoryStreamFactory.Default"/>
		/// </summary>
		public T MemoryStreamFactory(IMemoryStreamFactory memoryStreamFactory) => Assign(memoryStreamFactory, (a, v) => a._memoryStreamFactory = v);

		public T EnableTcpStats(bool enableTcpStats = true) => Assign(enableTcpStats, (a, v) => a._enableTcpStats = v);

		public T EnableThreadPoolStats(bool enableThreadPoolStats = true) => Assign(enableThreadPoolStats, (a, v) => a._enableThreadPoolStats = v);

		protected virtual void DisposeManagedResources()
		{
			_connectionPool?.Dispose();
			_connection?.Dispose();
			_semaphore?.Dispose();
			_proxyPassword?.Dispose();
			_basicAuthCredentials?.Dispose();
			_apiKeyAuthCredentials?.Dispose();
		}

		protected virtual bool HttpStatusCodeClassifier(HttpMethod method, int statusCode) =>
			statusCode >= 200 && statusCode < 300;

	}
}
