using System;
using System.Diagnostics;
using Elasticsearch.Net.Connection.HttpClient;
using Elasticsearch.Net.Connection.Thrift;
using Elasticsearch.Net;

namespace Nest.Tests.Integration
{
	public static class ElasticsearchConfiguration
	{
		public static readonly string DefaultIndex = Test.Default.DefaultIndex + "-" + Process.GetCurrentProcess().Id.ToString();

		public static Uri CreateBaseUri(int? port = null)
		{
			var host = Test.Default.Host;
			if (port == null && Process.GetProcessesByName("fiddler").HasAny())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}
		public static ConnectionSettings Settings(int? port = null)
		{

			return new ConnectionSettings(CreateBaseUri(port), ElasticsearchConfiguration.DefaultIndex)
				.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
				.UsePrettyResponses()
				.ExposeRawResponse();
		}

		public static readonly ElasticClient Client = new ElasticClient(Settings());
		public static readonly ElasticClient ClientNoRawResponse = new ElasticClient(Settings().ExposeRawResponse(false));
		public static readonly ElasticClient ClientThatTrows = new ElasticClient(Settings().ThrowOnElasticsearchServerExceptions());
		public static readonly ElasticClient ThriftClient = new ElasticClient(Settings(9500), new ThriftConnection(Settings(9500)));
	    public static readonly ElasticClient HttpClientClient = new ElasticClient(Settings(), new ElasticsearchHttpClient(Settings()));
		public static string NewUniqueIndexName()
		{
			return DefaultIndex + "_" + Guid.NewGuid().ToString();
		}

	}
}