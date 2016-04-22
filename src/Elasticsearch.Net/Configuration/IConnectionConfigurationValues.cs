using System;
using System.Collections.Specialized;
using System.Threading;

namespace Elasticsearch.Net
{
	public interface IConnectionConfigurationValues : IDisposable
	{
		/// <summary> Provides a semaphoreslim to transport implementations that need to limit access to a resource</summary>
		SemaphoreSlim BootstrapLock { get; }

		/// <summary> The connection pool to use when talking with elasticsearch </summary>
		IConnectionPool ConnectionPool { get; }

		/// <summary> The connection implementation to use when talking with elasticsearch </summary>
		IConnection Connection { get; }

		/// <summary>The serializer to use to serialize requests and deserialize responses</summary>
		IElasticsearchSerializer Serializer { get; }

		/// <summary>
		/// The timeout in milliseconds for each request to Elasticsearch
		/// </summary>
		TimeSpan RequestTimeout { get; }

		/// <summary>
		/// The timeout in milliseconds to use for ping requests, which are issued to determine whether a node is alive
		/// </summary>
		TimeSpan? PingTimeout { get; }

		/// <summary>
		/// The time to put dead nodes out of rotation (this will be multiplied by the number of times they've been dead)
		/// </summary>
		TimeSpan? DeadTimeout { get; }

		/// <summary>
		/// The maximum ammount of time a node is allowed to marked dead
		/// </summary>
		TimeSpan? MaxDeadTimeout { get; }

		/// <summary>
		/// Limits the total runtime including retries separately from <see cref="RequestTimeout"/>
		/// <pre>
		/// When not specified defaults to <see cref="RequestTimeout"/> which itself defaults to 60 seconds
		/// </pre>
		/// </summary>
		TimeSpan? MaxRetryTimeout { get; }

		/// <summary>
		/// When a retryable exception occurs or status code is returned this controls the maximum
		/// amount of times we should retry the call to elasticsearch
		/// </summary>
		int? MaxRetries { get; }

		/// <summary>
		/// This signals that we do not want to send initial pings to unknown/previously dead nodes
		/// and just send the call straightaway
		/// </summary>
		bool DisablePings { get; }

		/// <summary>
		/// Enable gzip compressed requests and responses, do note that you need to configure elasticsearch to set this
		/// <para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-http.html</para>
		/// </summary>
		bool EnableHttpCompression { get; }

		/// <summary>
		/// When set will force all connections through this proxy
		/// </summary>
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }

		/// <summary>
		/// Forces all requests to have ?pretty=true, causing elasticsearch to return formatted json.
		/// Also forces the client to send out formatted json. Defaults to false
		/// </summary>
		bool PrettyJson { get; }

		/// <summary>
		/// When set to true will disable (de)serializing directly to the request and response stream and return a byte[]
		/// copy of the raw request and response on elasticsearch calls. Defaults to  false
		/// </summary>
		bool DisableDirectStreaming { get; }

		/// <summary>
		/// Disabled proxy detection on the webrequest, in some cases this may speed up the first connection
		/// your appdomain makes, in other cases it will actually increase the time for the first connection.
		/// No silver bullet! use with care!
		/// </summary>
		bool DisableAutomaticProxyDetection { get; }

		/// <summary>
		/// By default the client enables http pipelining as elasticsearch 2.0 defaults to true as well
		/// </summary>
		bool HttpPipeliningEnabled { get; }

		/// <summary>
		/// Instead of following a c/go like error checking on response.IsValid always throw an exception
		/// on the client when a call resulted in an exception on either the client or the Elasticsearch server.
		/// <para>Reasons for such exceptions could be search parser errors, index missing exceptions, etc...</para>
		/// </summary>
		bool ThrowExceptions { get; }

		/// <summary>
		/// Sniff the cluster state immediatly on startup
		/// </summary>
		bool SniffsOnStartup { get; }

		/// <summary>
		/// Force a new sniff for the cluster state everytime a connection dies
		/// </summary>
		bool SniffsOnConnectionFault { get; }

		/// <summary>
		/// Force a new sniff for the cluster when the cluster state information is older than
		/// the specified timespan
		/// </summary>
		TimeSpan? SniffInformationLifeSpan { get; }

		/// <summary>
		/// Append these query string parameters automatically to every request
		/// </summary>
		NameValueCollection QueryStringParameters { get; }

		/// <summary>
		/// Try to send these headers for every request
		/// </summary>
		NameValueCollection Headers { get; }

		/// <summary>
		/// Allows you to register a callback every time a an API call is returned
		/// </summary>
		Action<IApiCallDetails> OnRequestCompleted { get; }

		/// <summary>
		/// Basic access authorization credentials to specify with all requests.
		/// </summary>
		BasicAuthenticationCredentials BasicAuthenticationCredentials { get; }

		/// <summary>
		/// KeepAliveTime - specifies the timeout, in milliseconds, with no
		/// activity until the first keep-alive packet is sent.
		/// </summary>
		TimeSpan? KeepAliveTime { get; }

		/// <summary>
		/// KeepAliveInterval - specifies the interval, in milliseconds, between
		/// when successive keep-alive packets are sent if no acknowledgement is
		/// received.
		/// </summary>
		TimeSpan? KeepAliveInterval { get; }
	}
}
