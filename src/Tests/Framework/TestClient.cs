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
using System.IO;
using System.Text;

namespace Tests.Framework
{
	public static class TestClient
	{
		private static bool _integrationOverride = false;
		private static string _manualOverrideVersion = "2.0.0-rc1";

		private static string ElasticVersionInEnvironment = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");

		public static string ElasticsearchVersion => ElasticVersionInEnvironment ?? (_integrationOverride ? _manualOverrideVersion : null);

		public static bool RunIntegrationTests => _integrationOverride || !string.IsNullOrEmpty(ElasticsearchVersion);

		public static bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		public static ConnectionSettings CreateSettings(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200)
		{
			var defaultSettings = new ConnectionSettings(new SingleNodeConnectionPool(CreateNode(port)), CreateConnection())
				.SetDefaultIndex("default-index")
				.PrettyJson()
				.InferMappingFor<Project>(map => map
					.IndexName("project")
					.IdProperty(p => p.Name)
				)
				.InferMappingFor<CommitActivity>(map => map
					.IndexName("project")
					.TypeName("commits")
				)
				.InferMappingFor<Developer>(map => map
					.Ignore(p => p.PrivateValue)
					.Rename(p => p.OnlineHandle, "nickname")
				)
				//We try and fetch the test name during integration tests when running fiddler to send the name 
				//as the TestMethod header, this allows us to quickly identify which test sent which request
				.SetGlobalHeaders(new NameValueCollection { { "TestMethod", ExpensiveTestNameForIntegrationTests() } });
			
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port));

		public static Uri CreateNode(int? port = null) => 
			new UriBuilder("http", (RunningFiddler) ? "ipv4.fiddler" : "localhost", port.GetValueOrDefault(9200)).Uri;

		public static IConnection CreateConnection() => RunIntegrationTests ? new HttpConnection() : new InMemoryConnection();

		public static IElasticClient GetFixedReturnClient(object responseJson)
		{
			var serializer = new NestSerializer(new ConnectionSettings());
			string fixedResult = string.Empty;
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(responseJson, ms);
				fixedResult =Encoding.UTF8.GetString(ms.ToArray());
			}
			var connection = new InMemoryConnection(fixedResult);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, connection);
			return new ElasticClient(settings);
		}



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