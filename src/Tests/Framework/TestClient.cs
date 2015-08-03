using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net.Connection;
using Nest;
using Tests.Framework.MockData;
using Elasticsearch.Net.ConnectionPool;

namespace Tests.Framework
{
	public static class TestClient
	{
		private static bool _integrationOverride = true;
		private static string _manualOverrideVersion = "2.0.0";

		public static string ElasticsearchVersion => 
			Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION") ?? (_integrationOverride ? _manualOverrideVersion : null);

		public static bool RunIntegrationTests => _integrationOverride || !string.IsNullOrEmpty(ElasticsearchVersion);

		public static bool RunningFiddler => Process.GetProcessesByName("fiddler").Any();

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200 )
		{
			var defaultSettings = new ConnectionSettings(new SingleNodeConnectionPool(CreateNode(port)), CreateConnection())
				.SetDefaultIndex("default-index")
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
				.SetGlobalHeaders(new NameValueCollection { {"TestMethod", ExpensiveTestNameForIntegrationTests()} });

			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return new ElasticClient(settings);
		}

		public static Uri CreateNode(int? port = null) => 
			new UriBuilder("http", (RunningFiddler) ? "ipv4.fiddler" : "localhost", port.GetValueOrDefault(9200)).Uri;

		public static IConnection CreateConnection() => RunIntegrationTests ? new HttpConnection() : new InMemoryConnection();

		public static string ExpensiveTestNameForIntegrationTests()
		{
			if (!(RunningFiddler && RunIntegrationTests)) return "ignore";

			var st = new StackTrace();
			var types = GetTypes(st);
			return (types.Select(f=>f.FullName).LastOrDefault()  ?? "Seeder").Split('.').Last();
		}

		private static List<Type> GetTypes(StackTrace st)
		{
			var types = (from f in st.GetFrames()
				let method = f.GetMethod()
				where method != null
				let type = method.DeclaringType
				where type.FullName.StartsWith("Tests.") && !type.FullName.StartsWith("Tests.Framework.")
				select type).ToList();
			return types;
		}

	}
}