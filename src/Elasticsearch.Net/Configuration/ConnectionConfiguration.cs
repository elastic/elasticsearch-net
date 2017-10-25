using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

#if DOTNETCORE
using System.Net;
using System.Net.Http;
#endif


namespace Elasticsearch.Net
{
	/// <summary>
	/// ConnectionConfiguration allows you to control how ElasticLowLevelClient behaves and where/how it connects
	/// to elasticsearch
	/// </summary>
	public class ConnectionConfiguration : ConnectionConfiguration<ConnectionConfiguration>
	{
		internal static bool IsCurlHandler { get; } =
            #if DOTNETCORE
                typeof(HttpClientHandler).Assembly().GetType("System.Net.Http.CurlHandler") != null;
            #else
                 false;
            #endif
		public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);
		public static readonly TimeSpan DefaultPingTimeout = TimeSpan.FromSeconds(2);
		public static readonly TimeSpan DefaultPingTimeoutOnSSL = TimeSpan.FromSeconds(5);
		public static readonly int DefaultConnectionLimit = IsCurlHandler ? Environment.ProcessorCount : 80;

		/// <summary>
		/// ConnectionConfiguration allows you to control how ElasticLowLevelClient behaves and where/how it connects
		/// to elasticsearch
		/// </summary>
		/// <param name="uri">The root of the elasticsearch node we want to connect to. Defaults to http://localhost:9200</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public ConnectionConfiguration(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")))
		{ }

		/// <summary>
		/// ConnectionConfiguration allows you to control how ElasticLowLevelClient behaves and where/how it connects
		/// to elasticsearch
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that'll tell the client what nodes are available</param>
		public ConnectionConfiguration(IConnectionPool connectionPool)
			// ReSharper disable once IntroduceOptionalParameters.Global
			: this(connectionPool, null, null)
		{ }

		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection)
			// ReSharper disable once IntroduceOptionalParameters.Global
			: this(connectionPool, connection, null)
		{ }

		public ConnectionConfiguration(IConnectionPool connectionPool, IElasticsearchSerializer serializer)
			: this(connectionPool, null, serializer)
		{ }

		// ReSharper disable once MemberCanBePrivate.Global
		// eventhough we use don't use this we very much would like to  expose this constructor

		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection, IElasticsearchSerializer serializer)
			: base(connectionPool, connection, serializer)
		{ }
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionConfiguration<T> : IConnectionConfigurationValues, IHideObjectMembers
		where T : ConnectionConfiguration<T>
	{
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		SemaphoreSlim IConnectionConfigurationValues.BootstrapLock => this._semaphore;

		private TimeSpan _requestTimeout;
		TimeSpan IConnectionConfigurationValues.RequestTimeout => _requestTimeout;

		private TimeSpan? _pingTimeout;
		TimeSpan? IConnectionConfigurationValues.PingTimeout => _pingTimeout;

		private TimeSpan? _deadTimeout;
		TimeSpan? IConnectionConfigurationValues.DeadTimeout => _deadTimeout;

		private TimeSpan? _maxDeadTimeout;
		TimeSpan? IConnectionConfigurationValues.MaxDeadTimeout => _maxDeadTimeout;

		private TimeSpan? _maxRetryTimeout;
		TimeSpan? IConnectionConfigurationValues.MaxRetryTimeout => _maxRetryTimeout;

		private TimeSpan? _keepAliveTime;
		TimeSpan? IConnectionConfigurationValues.KeepAliveTime => _keepAliveTime;

		private TimeSpan? _keepAliveInterval;
		TimeSpan? IConnectionConfigurationValues.KeepAliveInterval => _keepAliveInterval;

		private string _proxyUsername;
		string IConnectionConfigurationValues.ProxyUsername => _proxyUsername;

		private string _proxyPassword;
		string IConnectionConfigurationValues.ProxyPassword => _proxyPassword;

		private bool _disablePings;
		bool IConnectionConfigurationValues.DisablePings => _disablePings;

		private string _proxyAddress;
		string IConnectionConfigurationValues.ProxyAddress => _proxyAddress;

		private bool _prettyJson;
		bool IConnectionConfigurationValues.PrettyJson => _prettyJson;

		private bool _disableDirectStreaming = false;
		bool IConnectionConfigurationValues.DisableDirectStreaming => _disableDirectStreaming;

		private bool _disableAutomaticProxyDetection = false;
		bool IConnectionConfigurationValues.DisableAutomaticProxyDetection => _disableAutomaticProxyDetection;

		private int? _maxRetries;
		int? IConnectionConfigurationValues.MaxRetries => _maxRetries;

		private int _connectionLimit;
		int IConnectionConfigurationValues.ConnectionLimit => _connectionLimit;

		private bool _sniffOnStartup;
		bool IConnectionConfigurationValues.SniffsOnStartup => _sniffOnStartup;

		private bool _sniffOnConnectionFault;
		bool IConnectionConfigurationValues.SniffsOnConnectionFault => _sniffOnConnectionFault;

		private TimeSpan? _sniffLifeSpan;
		TimeSpan? IConnectionConfigurationValues.SniffInformationLifeSpan => _sniffLifeSpan;

		private bool _enableHttpCompression;
		bool IConnectionConfigurationValues.EnableHttpCompression => _enableHttpCompression;

		private bool _enableHttpPipelining = true;
		bool IConnectionConfigurationValues.HttpPipeliningEnabled => _enableHttpPipelining;

		private bool _throwExceptions;
		bool IConnectionConfigurationValues.ThrowExceptions => _throwExceptions;

		private static void DefaultCompletedRequestHandler(IApiCallDetails response) { }
		Action<IApiCallDetails> _completedRequestHandler = DefaultCompletedRequestHandler;
		Action<IApiCallDetails> IConnectionConfigurationValues.OnRequestCompleted => _completedRequestHandler;

		private static void DefaultRequestDataCreated(RequestData response) { }
		private Action<RequestData> _onRequestDataCreated = DefaultRequestDataCreated;
		Action<RequestData> IConnectionConfigurationValues.OnRequestDataCreated => _onRequestDataCreated;

		private Func<object, X509Certificate,X509Chain,SslPolicyErrors, bool> _serverCertificateValidationCallback;
		Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> IConnectionConfigurationValues.ServerCertificateValidationCallback => _serverCertificateValidationCallback;

		private X509CertificateCollection _clientCertificates;
		X509CertificateCollection IConnectionConfigurationValues.ClientCertificates => _clientCertificates;

		/// <summary>
		/// The default predicate for <see cref="IConnectionPool"/> implementations that return true for <see cref="IConnectionPool.SupportsReseeding"/>
		/// in which case master only nodes are excluded from API calls.
		/// </summary>
		private static bool DefaultReseedableNodePredicate(Node node) => !node.MasterOnlyNode;
		private static bool DefaultNodePredicate(Node node) => true;
		private Func<Node, bool> _nodePredicate = DefaultNodePredicate;
		Func<Node, bool> IConnectionConfigurationValues.NodePredicate => _nodePredicate;

		private readonly NameValueCollection _queryString = new NameValueCollection();
		NameValueCollection IConnectionConfigurationValues.QueryStringParameters => _queryString;

		private readonly NameValueCollection _headers = new NameValueCollection();
		NameValueCollection IConnectionConfigurationValues.Headers => _headers;

		BasicAuthenticationCredentials _basicAuthCredentials;
		BasicAuthenticationCredentials IConnectionConfigurationValues.BasicAuthenticationCredentials => _basicAuthCredentials;

		protected IElasticsearchSerializer _requestResponseSerializer;
		IElasticsearchSerializer IConnectionConfigurationValues.RequestResponseSerializer => _requestResponseSerializer;

		private readonly IConnectionPool _connectionPool;
		IConnectionPool IConnectionConfigurationValues.ConnectionPool => _connectionPool;

		private readonly IConnection _connection;
		IConnection IConnectionConfigurationValues.Connection => _connection;

		protected ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection, IElasticsearchSerializer requestResponseSerializer)
		{
			this._connectionPool = connectionPool;
			this._connection = connection ?? new HttpConnection();
			this._requestResponseSerializer = requestResponseSerializer ?? new LowLevelRequestResponseSerializer();

			this._connectionLimit = ConnectionConfiguration.DefaultConnectionLimit;
			this._requestTimeout = ConnectionConfiguration.DefaultTimeout;
			this._sniffOnConnectionFault = true;
			this._sniffOnStartup = true;
			this._sniffLifeSpan = TimeSpan.FromHours(1);
			if (this._connectionPool.SupportsReseeding)
				this._nodePredicate = DefaultReseedableNodePredicate;
		}

		private T Assign(Action<ConnectionConfiguration<T>> assigner) => Fluent.Assign((T)this, assigner);

		/// <summary>
		/// The default serializer used to serialize documents to and from JSON
		/// </summary>
		protected virtual IElasticsearchSerializer DefaultSerializer(T settings) => new LowLevelRequestResponseSerializer();

		/// <summary>
		/// Sets the keep-alive option on a TCP connection.
		/// <para>For Desktop CLR, sets ServicePointManager.SetTcpKeepAlive</para>
		/// </summary>
		/// <param name="keepAliveTime">Specifies the timeout with no activity until the first keep-alive packet is sent.</param>
		/// <param name="keepAliveInterval">Specifies the interval between when successive keep-alive packets are sent if no acknowledgement is received.</param>
		public T EnableTcpKeepAlive(TimeSpan keepAliveTime, TimeSpan keepAliveInterval) =>
			Assign(a => { this._keepAliveTime = keepAliveTime; this._keepAliveInterval = keepAliveInterval; });

		/// <summary>
		/// The maximum number of retries for a given request,
		/// </summary>
		public T MaximumRetries(int maxRetries) => Assign(a => a._maxRetries = maxRetries);

        /// <summary>
        /// Limits the number of concurrent connections that can be opened to an endpoint. Defaults to <c>80</c>.
        /// <para>For Desktop CLR, this setting applies to the DefaultConnectionLimit property on the  ServicePointManager object when creating ServicePoint objects, affecting the default <see cref="IConnection"/> implementation.</para>
        /// <para>For Core CLR, this setting applies to the MaxConnectionsPerServer property on the HttpClientHandler instances used by the HttpClient inside the default <see cref="IConnection"/> implementation</para>
        /// </summary>
        /// <param name="connectionLimit">The connection limit, a value lower then 0 will cause the connection limit not to be set at all</param>
        public T ConnectionLimit(int connectionLimit)
		{
			return Assign(a => a._connectionLimit = connectionLimit);
		}

        /// <summary>
        /// Enables resniffing of the cluster when a call fails, if the connection pool supports reseeding. Defaults to <c>true</c>
        /// </summary>
        public T SniffOnConnectionFault(bool sniffsOnConnectionFault = true) => Assign(a => a._sniffOnConnectionFault = sniffsOnConnectionFault);

        /// <summary>
        /// Enables sniffing on first usage of a connection pool if that pool supports reseeding. Defaults to <c>true</c>
        /// </summary>
        public T SniffOnStartup(bool sniffsOnStartup = true) => Assign(a => a._sniffOnStartup = sniffsOnStartup);

		/// <summary>
		/// Set the duration after which a cluster state is considered stale and a sniff should be performed again.
		/// An <see cref="IConnectionPool"/> has to signal it supports reseeding, otherwise sniffing will never happen.
		/// Defaults to 1 hour.
		/// Set to null to disable completely. Sniffing will only ever happen on ConnectionPools that return true for SupportsReseeding
		/// </summary>
		/// <param name="sniffLifeSpan">The duration a clusterstate is considered fresh, set to null to disable periodic sniffing</param>
		public T SniffLifeSpan(TimeSpan? sniffLifeSpan) => Assign(a => a._sniffLifeSpan = sniffLifeSpan);

		/// <summary>
		/// Enables gzip compressed requests and responses.
		/// <para>IMPORTANT: You need to configure http compression on Elasticsearch to be able to use this</para>
		/// <para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-http.html</para>
		/// </summary>
		public T EnableHttpCompression(bool enabled = true) => Assign(a => a._enableHttpCompression = enabled);

		/// <summary>
		/// Disables the automatic detection of a proxy
		/// </summary>
		public T DisableAutomaticProxyDetection(bool disable = true) => Assign(a => a._disableAutomaticProxyDetection = disable);

		/// <summary>
		/// Instead of following a c/go like error checking on response.IsValid always throw an exception
		/// on the client when a call resulted in an exception on either the client or the Elasticsearch server.
		/// <para>Reasons for such exceptions could be search parser errors, index missing exceptions, etc...</para>
		/// </summary>
		public T ThrowExceptions(bool alwaysThrow = true) => Assign(a => a._throwExceptions = alwaysThrow);

		/// <summary>
		/// When a node is used for the very first time or when it's used for the first time after it has been marked dead
		/// a ping with a very low timeout is send to the node to make sure that when it's still dead it reports it as fast as possible.
		/// You can disable these pings globally here if you rather have it fail on the possible slower original request
		/// </summary>
		public T DisablePing(bool disable = true) => Assign(a => a._disablePings = disable);

		/// <summary>
		/// A collection of query string parameters that will be sent with every request. Useful in situations where you always need to pass a parameter e.g. an API key.
		/// </summary>
		public T GlobalQueryStringParameters(NameValueCollection queryStringParameters) => Assign(a => a._queryString.Add(queryStringParameters));

		/// <summary>
		/// A collection of headers that will be sent with every request. Useful in situations where you always need to pass a header e.g. a custom auth header
		/// </summary>
		public T GlobalHeaders(NameValueCollection headers) => Assign(a => a._headers.Add(headers));

        /// <summary>
        /// Sets the default timeout in milliseconds for each request to Elasticsearch. Defaults to <c>60</c> seconds.
        /// <para>NOTE: You can set this to a high value here, and specify a timeout on Elasticsearch's side.</para>
        /// </summary>
        /// <param name="timeout">time out in milliseconds</param>
        public T RequestTimeout(TimeSpan timeout) => Assign(a => a._requestTimeout = timeout);

        /// <summary>
        /// Sets the default ping timeout in milliseconds for ping requests, which are used
        /// to determine whether a node is alive. Pings should fail as fast as possible.
        /// </summary>
        /// <param name="timeout">The ping timeout in milliseconds defaults to <c>1000</c>, or <c>2000</c> if using SSL.</param>
        public T PingTimeout(TimeSpan timeout) => Assign(a => a._pingTimeout = timeout);

		/// <summary>
		/// Sets the default dead timeout factor when a node has been marked dead.
		/// </summary>
		/// <remarks>Some connection pools may use a flat timeout whilst others take this factor and increase it exponentially</remarks>
		/// <param name="timeout"></param>
		public T DeadTimeout(TimeSpan timeout) => Assign(a => a._deadTimeout = timeout);

		/// <summary>
		/// Sets the maximum time a node can be marked dead.
		/// Different implementations of <see cref="IConnectionPool"/> may choose a different default.
		/// </summary>
		/// <param name="timeout">The timeout in milliseconds</param>
		public T MaxDeadTimeout(TimeSpan timeout) => Assign(a => a._maxDeadTimeout = timeout);

        /// <summary>
        /// Limits the total runtime, including retries, separately from <see cref="RequestTimeout"/>
        /// <para>
        /// When not specified, defaults to <see cref="RequestTimeout"/>, which itself defaults to <c>60</c> seconds
        /// </para>
        /// </summary>
        public T MaxRetryTimeout(TimeSpan maxRetryTimeout) => Assign(a => a._maxRetryTimeout = maxRetryTimeout);

		/// <summary>
		/// If your connection has to go through proxy, use this method to specify the proxy url
		/// </summary>
		public T Proxy(Uri proxyAdress, string username, string password)
		{
			proxyAdress.ThrowIfNull(nameof(proxyAdress));
			this._proxyAddress = proxyAdress.ToString();
			this._proxyUsername = username;
			this._proxyPassword = password;
			return (T)this;
		}

		/// <summary>
		/// Forces all requests to have ?pretty=true querystring parameter appended,
		/// causing Elasticsearch to return formatted JSON.
		/// Also forces the client to send out formatted JSON. Defaults to <c>false</c>
		/// </summary>
		public T PrettyJson(bool b = true) => Assign(a =>
		{
			this._prettyJson = b;
			if (!b && this._queryString["pretty"] != null) this._queryString.Remove("pretty");
			else if (b && this._queryString["pretty"] == null)
				this.GlobalQueryStringParameters(new NameValueCollection { { "pretty", b.ToString().ToLowerInvariant() } });
		});

		/// <summary>
		/// Ensures the response bytes are always available on the <see cref="ElasticsearchResponse{T}"/>
		/// <para>IMPORTANT: Depending on the registered serializer,
		/// this may cause the response to be buffered in memory first, potentially affecting performance.</para>
		/// </summary>
		public T DisableDirectStreaming(bool b = true) => Assign(a => a._disableDirectStreaming = b);

		/// <summary>
		/// Registers an <see cref="Action{IApiCallDetails}"/> that is called when a response is received from Elasticsearch.
		/// This can be useful for implementing custom logging.
		/// Multiple callbacks can be registered by calling this multiple times
		/// </summary>
		public T OnRequestCompleted(Action<IApiCallDetails> handler) =>
			Assign(a => a._completedRequestHandler += handler ?? DefaultCompletedRequestHandler);

		/// <summary>
		/// Registers an <see cref="Action{RequestData}"/> that is called when <see cref="RequestData"/> is created.
		/// Multiple callbacks can be registered by calling this multiple times
		/// </summary>
		public T OnRequestDataCreated(Action<RequestData> handler) =>
			Assign(a => a._onRequestDataCreated += handler ?? DefaultRequestDataCreated);

		/// <summary>
		/// Basic Authentication credentials to send with all requests to Elasticsearch
		/// </summary>
		public T BasicAuthentication(string userName, string password) =>
			Assign(a => a._basicAuthCredentials = new BasicAuthenticationCredentials
			{
				Username = userName,
				Password = password
			});

		/// <summary>
		/// Allows for requests to be pipelined. http://en.wikipedia.org/wiki/HTTP_pipelining
		/// <para>NOTE: HTTP pipelining must also be enabled in Elasticsearch for this to work properly.</para>
		/// </summary>
		public T EnableHttpPipelining(bool enabled = true) => Assign(a => a._enableHttpPipelining = enabled);

		/// <summary>
		/// Register a predicate to select which nodes that you want to execute API calls on. Note that sniffing requests omit this predicate and always execute on all nodes.
		/// When using an <see cref="IConnectionPool"/> implementation that supports reseeding of nodes, this will default to omitting master only node from regular API calls.
		/// When using static or single node connection pooling it is assumed the list of node you instantiate the client with should be taken verbatim.
		/// </summary>
		/// <param name="predicate">Return true if you want the node to be used for API calls</param>
		public T NodePredicate(Func<Node, bool> predicate)
		{
			if (predicate == null) return (T) this;
			this._nodePredicate = predicate;
			return (T)this;
		}

		/// <summary>
		/// Turns on settings that aid in debugging like DisableDirectStreaming() and PrettyJson()
		/// so that the original request and response JSON can be inspected.
		/// </summary>
		/// <param name="onRequestCompleted">
		/// An optional callback to be performed when the request completes. This will
		/// not overwrite the global OnRequestCompleted callback that is set directly on
		/// ConnectionSettings. If no callback is passed, DebugInformation from the response
		/// will be written to the debug output by default.
		/// </param>
		public T EnableDebugMode(Action<IApiCallDetails> onRequestCompleted = null)
		{
			this._disableDirectStreaming = true;
			this._prettyJson = true;

			var originalCompletedRequestHandler = this._completedRequestHandler;
			var debugCompletedRequestHandler = onRequestCompleted ?? (d => Debug.WriteLine(d.DebugInformation));
			this._completedRequestHandler = d =>
			{
				originalCompletedRequestHandler?.Invoke(d);
				debugCompletedRequestHandler?.Invoke(d);
			};
			return (T)this;
		}

		/// <summary>
		/// Register a ServerCertificateValidationCallback, this is called per endpoint until it returns true.
		/// After this callback returns true that endpoint is validated for the lifetime of the ServiceEndpoint
		/// for that host.
		/// </summary>
		public T ServerCertificateValidationCallback(Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> callback) =>
			Assign(a => a._serverCertificateValidationCallback = callback);

		/// <summary>
		/// Use the following certificates to authenticate all HTTP requests. You can also set them on individual
		/// request using <see cref="RequestConfiguration.ClientCertificates"/>
		/// </summary>
		public T ClientCertificates(X509CertificateCollection certificates) =>
			Assign(a => a._clientCertificates = certificates);

		/// <summary>
		/// Use the following certificate to authenticate all HTTP requests. You can also set them on individual
		/// request using <see cref="RequestConfiguration.ClientCertificates"/>
		/// </summary>
		public T ClientCertificate(X509Certificate certificate) =>
			Assign(a => a._clientCertificates = new X509Certificate2Collection { certificate });

		/// <summary>
		/// Use the following certificate to authenticate all HTTP requests. You can also set them on individual
		/// request using <see cref="RequestConfiguration.ClientCertificates"/>
		/// </summary>
		public T ClientCertificate(string certificatePath) =>
			Assign(a => a._clientCertificates = new X509Certificate2Collection { new X509Certificate(certificatePath) });

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources()
		{
			this._connectionPool?.Dispose();
			this._connection?.Dispose();
			this._semaphore?.Dispose();
		}
	}
}

