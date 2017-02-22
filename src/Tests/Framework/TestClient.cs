using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Elasticsearch.Net;
using Nest_5_2_0;
using Tests.Framework.Configuration;
using Tests.Framework.MockData;
using Tests.Framework.Versions;

namespace Tests.Framework
{
	public static class TestClient
	{
		public static bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		public static ITestConfiguration Configuration = LoadConfiguration();
		public static ConnectionSettings GlobalDefaultSettings = CreateSettings();
		public static IElasticClient Default = new ElasticClient(GlobalDefaultSettings);
		public static IElasticClient DefaultInMemoryClient = GetInMemoryClient();

		public static Uri CreateUri(int port = 9200) => new UriBuilder("http", Host, port).Uri;

		public static string Host => (RunningFiddler) ? "ipv4.fiddler" : "localhost";

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
				: @"../../../tests.yaml";

			var fullPath = Path.GetFullPath(yamlConfigurationPath);

			if (!File.Exists(fullPath))
				throw new Exception($"Tried to load yaml from {fullPath} but it does not exist : pwd:{directoryInfo.FullName}");

			return new YamlConfiguration(yamlConfigurationPath);
		}

		private static ConnectionSettings DefaultSettings(ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.PrettyJson()
			.InferMappingFor<Project>(ProjectMapping)
			.InferMappingFor<CommitActivity>(map => map
				.IndexName("project")
				.TypeName("commits")
			)
			.InferMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.Rename(p => p.OnlineHandle, "nickname")
			)
			.InferMappingFor<PercolatedQuery>(map => map
				.IndexName("queries")
				.TypeName(PercolatorType)
			)
			//.EnableTcpKeepAlive(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2))
			//.PrettyJson()
			//TODO make this random
			//.EnableHttpCompression()

			.OnRequestDataCreated(data=> data.Headers.Add("TestMethod", ExpensiveTestNameForIntegrationTests()));

		private static IClrTypeMapping<Project> ProjectMapping(ClrTypeMappingDescriptor<Project> m)
		{
			m.IndexName("project").IdProperty(p => p.Name);
			//*_range type only available since 5.2.0 so we ignore them when running integration tests
			if (VersionUnderTestSatisfiedBy("<5.2.0") && Configuration.RunIntegrationTests)
				m.Ignore(p => p.Ranges);
			return m;
		}

		public static bool VersionUnderTestSatisfiedBy(string versionRange) =>
			new SemVer.Range(versionRange).IsSatisfied(TestClient.Configuration.ElasticsearchVersion);

		public static string PercolatorType => Configuration.ElasticsearchVersion <= new ElasticsearchVersion("5.0.0-alpha1") ? ".percolator" : "query";


		public static ConnectionSettings CreateSettings(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceInMemory = false,
			Func<Uri, IConnectionPool> createPool = null,
			Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory = null
			)
		{
			createPool = createPool ?? (u => new SingleNodeConnectionPool(u));
#pragma warning disable CS0618 // Type or member is obsolete
			var defaultSettings = DefaultSettings(new ConnectionSettings(createPool(CreateUri(port)), CreateConnection(forceInMemory: forceInMemory), serializerFactory));
#pragma warning restore CS0618 // Type or member is obsolete
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static IElasticClient GetInMemoryClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: true));

		public static IElasticClient GetInMemoryClientWithSerializerFactory(Func<ConnectionSettings, ConnectionSettings> modifySettings, Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory) =>
			new ElasticClient(CreateSettings(modifySettings, forceInMemory: true, serializerFactory: serializerFactory));

		public static IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200, Func<Uri, IConnectionPool> createPool = null) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: false, createPool: createPool));

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
			var serializer = Default.Serializer;
			var fixedResult = contentType == "application/json"
				? serializer.SerializeToBytes(response)
				: Encoding.UTF8.GetBytes(response.ToString());

			var connection = new InMemoryConnection(fixedResult, statusCode, exception);
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var defaultSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("default-index");
			var settings = (modifySettings != null) ? modifySettings(defaultSettings) : defaultSettings;
			return new ElasticClient(settings);
		}

		private static string ExpensiveTestNameForIntegrationTests()
		{
			if (!(RunningFiddler && Configuration.RunIntegrationTests)) return "ignore";

#if DOTNETCORE
			return "TODO: Work out how to get test name. Maybe Environment.StackTrace?";
#else
			var st = new StackTrace();
			var types = GetTypes(st);
			var name = types
				.LastOrDefault(type => type.FullName.StartsWith("Tests.") && !type.FullName.StartsWith("Tests.Framework."));
			return name?.FullName ?? string.Join(": ", types.Select(n=>n.Name));
#endif
		}

#if !DOTNETCORE
		private static List<Type> GetTypes(StackTrace st)
		{
			var types = (from f in st.GetFrames()
						 let method = f.GetMethod()
						 where method != null
						 let type = method.DeclaringType
						 where type != null
						 select type).ToList();
			return types;
		}
#endif

	}
}
