using System;
using System.Collections.Specialized;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	public interface IConnectionConfigurationValues
	{
		IConnectionPool ConnectionPool { get; }
		//Uri Uri { get; }
		int MaximumAsyncConnections { get; }
		//string Host { get; }
		//int Port { get; }
		int Timeout { get; }
		int? MaxRetries { get; }
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }

		bool TraceEnabled { get; }
		bool UriSpecifiedBasicAuth { get; }
		bool UsesPrettyResponses { get; }
	
		NameValueCollection QueryStringParameters { get; }
		Action<ElasticsearchResponse> ConnectionStatusHandler { get; }

		IElasticsearchSerializer Serializer { get; set; }
	}
}