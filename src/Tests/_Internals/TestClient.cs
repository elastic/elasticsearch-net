using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net.Connection;
using Nest;

namespace Tests._Internals
{
	public static class TestClient
	{
		private static bool _runIntegrationTests = false;

		public static string ElasticsearchVersion => Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");

		public static bool RunIntegrationTests => _runIntegrationTests || !string.IsNullOrEmpty(ElasticsearchVersion);

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200 )
		{
			var defaultSettings = new ConnectionSettings((CreateBaseUri(port)));
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return new ElasticClient(settings, CreateConnection(settings));
		}

		public static IConnection CreateConnection(IConnectionSettingsValues connectionSettings) =>
			RunIntegrationTests ? new HttpConnection(connectionSettings) : new InMemoryConnection(connectionSettings);

		public static Uri CreateBaseUri(int? port = null)
		{
			var host = "localhost";
			if (port != 9500 && Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}

	}
}