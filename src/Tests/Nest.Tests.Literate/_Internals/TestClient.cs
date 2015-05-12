using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net.Connection;

namespace Nest.Tests.Literate
{
	public static class TestClient
	{
		private static bool _runIntegrationTests = false;

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null)
		{
			var defaultSettings = new ConnectionSettings((CreateBaseUri()));
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

		public static bool RunIntegrationTests
		{
			get
			{
				if (_runIntegrationTests) return true;
#if INTEGRATIONTESTS
				return true;
#else
				return false;
#endif
			}
		}
	}
}