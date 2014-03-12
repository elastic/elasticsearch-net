using System;
using System.Collections.Specialized;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public interface IConnectionConfigurationValues
	{
		IConnectionPool ConnectionPool { get; }
		//Uri Uri { get; }
		int MaximumAsyncConnections { get; }
		//string Host { get; }
		//int Port { get; }
		int Timeout { get; }
		int? PingTimeout { get; }
		int? DeadTimeout { get; }
		int? MaxDeadTimeout { get; }
		int? MaxRetries { get; }
		bool DisablePings { get; }
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }

		bool TraceEnabled { get; }
		bool UriSpecifiedBasicAuth { get; }
		bool UsesPrettyResponses { get; }

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
		/// Connection status handler that will be called everytime the connection receives anything.
		/// </summary>
		Action<ElasticsearchResponse> ConnectionStatusHandler { get; }

		/// <summary>
		/// 
		/// </summary>
		IElasticsearchSerializer Serializer { get; set; }
	}
}