using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net.Connection;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class TestClient
	{
		private static bool _integrationOverride = true;
		private static string _manualOverrideVersion = "2.0.0";

		public static string ElasticsearchVersion => 
			Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION") ?? (_integrationOverride ? _manualOverrideVersion : null);

		public static bool RunIntegrationTests => _integrationOverride || !string.IsNullOrEmpty(ElasticsearchVersion);

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200 )
		{
			var defaultSettings = new ConnectionSettings((CreateBaseUri(port)), "defaultindex")
				.InferMappingFor<Project>(map=>map
					.IndexName("project")
					.IdProperty(p=>p.Name)
				)
				.InferMappingFor<CommitActivity>(map=>map
					.IndexName("project")
					.TypeName("commits")
				)
				.InferMappingFor<Developer>(map=>map
					.Ignore(p=>p.PrivateValue)
					.Rename(p=>p.OnlineHandle, "nickname")
				)
				;

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