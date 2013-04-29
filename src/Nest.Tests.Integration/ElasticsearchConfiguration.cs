using System;
using System.Diagnostics;
using Nest.Thrift;

namespace Nest.Tests.Integration
{
	public static class ElasticsearchConfiguration
	{
		public static readonly string DefaultIndex = Test.Default.DefaultIndex + "-" + Process.GetCurrentProcess().Id.ToString();

		public static IConnectionSettings Settings(int? port = null)
		{
			var host = Test.Default.Host;
			if (port == null && Process.GetProcessesByName("fiddler").HasAny())
				host = "localhost.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;

			return new ConnectionSettings(uri)
				.SetDefaultIndex(ElasticsearchConfiguration.DefaultIndex)
				.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
				.UsePrettyResponses();
		}

		public static readonly ElasticClient Client = new ElasticClient(Settings());
		public static readonly ElasticClient ThriftClient = new ElasticClient(Settings(9500), new ThriftConnection(Settings(9500)));

		public static string NewUniqueIndexName()
		{
			return DefaultIndex + "_" + Guid.NewGuid().ToString();
		}

	}
}