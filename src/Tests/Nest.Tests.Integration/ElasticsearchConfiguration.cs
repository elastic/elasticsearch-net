using System;
using System.Diagnostics;
using Elasticsearch.Net.Connection.Thrift;
using Elasticsearch.Net;

namespace Nest.Tests.Integration
{
	public static class ElasticsearchConfiguration
	{
		public static readonly string DefaultIndex = "nest-test-data-" + Process.GetCurrentProcess().Id.ToString();

		public static Uri CreateBaseUri(int? port = null)
		{
			var host = "localhost";
			if (port == null && Process.GetProcessesByName("fiddler").HasAny())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}
		public static ConnectionSettings Settings(int? port = null, Uri hostOverride = null)
		{

			return new ConnectionSettings(hostOverride ?? CreateBaseUri(port), ElasticsearchConfiguration.DefaultIndex)
				.SetMaximumAsyncConnections(20)
				.UsePrettyResponses()
				.ExposeRawResponse();
		}


		public static readonly Lazy<ElasticClient> _client = new Lazy<ElasticClient>(()=>new ElasticClient(Settings()));
		public static ElasticClient Client { get { return _client.Value;  } }
		
		public static readonly Lazy<ElasticClient> _clientNoRawResponse = new Lazy<ElasticClient>(
			() => new ElasticClient(Settings()
				.ExposeRawResponse(false)
			)
		);
		public static ElasticClient ClientNoRawResponse { get { return _clientNoRawResponse.Value;  } }
		
		public static readonly Lazy<ElasticClient> _clientThatTrows = new Lazy<ElasticClient>(
			() => new ElasticClient(Settings()
				.ThrowOnElasticsearchServerExceptions()
			)
		);
		public static ElasticClient ClientThatThrows { get { return _clientThatTrows.Value;  } }
		
		public static readonly Lazy<ElasticClient> _thriftClient = new Lazy<ElasticClient>(
			() => new ElasticClient(
				Settings(9500), 
				new ThriftConnection(Settings(9500))
			)
		);
		public static ElasticClient ThriftClient { get { return _thriftClient.Value;  } }

		public static string NewUniqueIndexName()
		{
			return DefaultIndex + "_" + Guid.NewGuid().ToString();
		}

	}
}