using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class TestClient
	{
		public static ITestConfiguration Configuration = LoadConfiguration();

		public static bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		private static ConnectionSettings DefaultSettings(ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
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
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.Rename(p => p.OnlineHandle, "nickname")
			)
			//We try and fetch the test name during integration tests when running fiddler to send the name
			//as the TestMethod header, this allows us to quickly identify which test sent which request
			.GlobalHeaders(new NameValueCollection
			{
				{ "TestMethod", ExpensiveTestNameForIntegrationTests() }
			});


		public static ConnectionSettings CreateSettings(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceInMemory = false,
			Func<Uri, IConnectionPool> createPool = null,
			Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory = null
			)
		{
			createPool = createPool ?? (u => new SingleNodeConnectionPool(u));
			var defaultSettings = DefaultSettings(new ConnectionSettings(createPool(CreateNode(port)), CreateConnection(forceInMemory: forceInMemory), serializerFactory));
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static IElasticClient GetInMemoryClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: true));

		public static IElasticClient GetInMemoryClient(Func<ConnectionSettings, ConnectionSettings> modifySettings, Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory) =>
			new ElasticClient(CreateSettings(modifySettings, forceInMemory: true, serializerFactory: serializerFactory));

		public static IElasticClient GetClient(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200, Func<Uri, IConnectionPool> createPool = null) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: false, createPool: createPool));

		public static Uri CreateNode(int? port = null) =>
			new UriBuilder("http", Host, port.GetValueOrDefault(9200)).Uri;

		public static string Host => (RunningFiddler) ? "ipv4.fiddler" : "localhost";

		public static IConnection CreateConnection(ConnectionSettings settings = null, bool forceInMemory = false) =>
			Configuration.RunIntegrationTests && !forceInMemory
				? ((IConnection)new HttpConnection())
				: new InMemoryConnection();

		public static IElasticClient GetFixedReturnClient(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = "application/json",
			Exception exception = null)
		{
			var serializer = new JsonNetSerializer(new ConnectionSettings());
			byte[] fixedResult;

			if (contentType == "application/json")
			{
				using (var ms = new MemoryStream())
				{
					serializer.Serialize(response, ms);
					fixedResult = ms.ToArray();
				}
			}
			else
			{
				fixedResult = Encoding.UTF8.GetBytes(response.ToString());
			}

			var connection = new InMemoryConnection(fixedResult, statusCode, exception);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var defaultSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("default-index");
			var settings = (modifySettings != null) ? modifySettings(defaultSettings) : defaultSettings;
			return new ElasticClient(settings);
		}

		public static string ExpensiveTestNameForIntegrationTests()
		{
			if (!(RunningFiddler && Configuration.RunIntegrationTests)) return "ignore";

#if DOTNETCORE
			return "TODO: Work out how to get test name. Maybe Environment.StackTrace?";
#else
			var st = new StackTrace();
			var types = GetTypes(st);
			return (types.Select(f => f.FullName).LastOrDefault() ?? "Seeder").Split('.').Last();
#endif
		}

#if !DOTNETCORE
		private static List<Type> GetTypes(StackTrace st)
		{
			var types = (from f in st.GetFrames()
						 let method = f.GetMethod()
						 where method != null
						 let type = method.DeclaringType
						 where type != null && type.FullName.StartsWith("Tests.") && !type.FullName.StartsWith("Tests.Framework.")
						 select type).ToList();
			return types;
		}
#endif

		private static ITestConfiguration LoadConfiguration()
		{
			// The build script sets a TARGET env variable, so if it exists then
			// we must be running tests from the build script
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TARGET")))
				return new EnvironmentConfiguration();

			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			var yamlConfigurationPath = directoryInfo.Name == "Tests" &&
										directoryInfo.Parent != null &&
										directoryInfo.Parent.Name == "src"
				? "tests.yaml"
				: @"..\..\..\tests.yaml";

			return new YamlConfiguration(yamlConfigurationPath);
		}
	}
}
