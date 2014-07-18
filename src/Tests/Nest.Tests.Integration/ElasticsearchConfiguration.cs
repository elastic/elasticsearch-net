using System;
using System.Diagnostics;
using Elasticsearch.Net.Connection.Thrift;
using Elasticsearch.Net;

namespace Nest.Tests.Integration
{
	public static class ElasticsearchConfiguration
	{
		public static readonly string DefaultIndexPrefix = "nest_test_data-";
		public static readonly string Host = "localhost";
		public static readonly int MaxConnections = 20;


		public static readonly string DefaultIndex = DefaultIndexPrefix + Process.GetCurrentProcess().Id;

		private static Version _currentVersion;
		public static Version CurrentVersion
		{
			get
			{
				if (_currentVersion == null)
					_currentVersion = GetCurrentVersion();

				return _currentVersion;
			}
		}

		public static Uri CreateBaseUri(int? port = null)
		{
			var host = Host;
			if (port != 9500 && Process.GetProcessesByName("fiddler").HasAny())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}
		public static ConnectionSettings Settings(int? port = null, Uri hostOverride = null)
		{

			return new ConnectionSettings(hostOverride ?? CreateBaseUri(port), ElasticsearchConfiguration.DefaultIndex)
				.SetMaximumAsyncConnections(MaxConnections)
				.UsePrettyResponses()
				.ExposeRawResponse();
		}

		public static readonly Lazy<ElasticClient> Client = new Lazy<ElasticClient>(()=> new ElasticClient(Settings()));
		public static readonly Lazy<ElasticClient> ClientNoRawResponse = new Lazy<ElasticClient>(()=> new ElasticClient(Settings().ExposeRawResponse(false)));
		public static readonly Lazy<ElasticClient> ClientThatThrows = new Lazy<ElasticClient>(()=> new ElasticClient(Settings().ThrowOnElasticsearchServerExceptions()));
		public static readonly Lazy<ElasticClient> ThriftClient = new Lazy<ElasticClient>(()=> new ElasticClient(Settings(9500), new ThriftConnection(Settings(9500))));
		public static string NewUniqueIndexName()
		{
			return DefaultIndex + "_" + Guid.NewGuid().ToString();
		}

		public static Version GetCurrentVersion()
		{
			dynamic info = Client.Value.Raw.Info().Response;
			var version = Version.Parse(info.version.number);

			return version;
		}
	}
}