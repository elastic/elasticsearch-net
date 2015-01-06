using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net.Connection;

namespace Nest.Tests.Literate
{
	public static class TestClient
	{
		private static bool _runIntegrationTests = false;

		public static ConnectionSettings Settings { get; private set; }
		public static IConnection Connection { get; private set; }
		public static IElasticClient Client { get; private set; }

		static TestClient()
		{
			Settings = new ConnectionSettings(CreateBaseUri());
			Client = new ElasticClient(Settings, Connection);
		}

		public static IConnection CreateConnection(IConnectionSettingsValues connectionSettings)
		{
			if (RunIntegrationTests()) return new HttpConnection(connectionSettings);
			return new HttpConnection(Settings);
		}


		public static Uri CreateBaseUri(int? port = null)
		{
			var host = "localhost";
			if (port != 9500 && Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}

		public static bool RunIntegrationTests()
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