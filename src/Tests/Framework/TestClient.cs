using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.SourceSerializers;
using Tests.Framework.MockData;
using Tests.Framework.Versions;

namespace Tests.Framework
{
	public static class TestClient
	{
		public static bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		public static ITestConfiguration Configuration = LoadConfiguration();
		public static ConnectionSettings GlobalDefaultSettings = CreateSettings();
		public static readonly IElasticClient Default = new ElasticClient(GlobalDefaultSettings);
		public static readonly IElasticClient DefaultInMemoryClient = GetInMemoryClient();
		public static readonly IElasticClient DefaultClientWithSourceSerializer = TestClient.GetInMemoryClientWithSourceSerializer(
			modifySettings: s => s,
			sourceSerializerFactory: (settings, builtin) =>
			{
				var customSourceSerializer = new TestSourceSerializer(builtin);
				return TestClient.Configuration.UsingCustomSourceSerializer ? customSourceSerializer : null;
			});

		public static Uri CreateUri(int port = 9200, bool forceSsl = false) =>
			new UriBuilder(forceSsl ? "https" : "http", Host, port).Uri;

		public static string DefaultHost => "localhost";
		public static string Host => (RunningFiddler) ? "ipv4.fiddler" : DefaultHost;

		private static ITestConfiguration LoadConfiguration()
		{
			// The build script sets a FAKEBUILD env variable, so if it exists then
			// we must be running tests from the build script
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FAKEBUILD")))
				return new EnvironmentConfiguration();

			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			var yamlConfigurationPath = (directoryInfo.Name == "Tests"
				&& directoryInfo.Parent != null
				&& directoryInfo.Parent.Name == "src")
				? "."
				: @"../../../";

			var localYamlFile = Path.GetFullPath(Path.Combine(yamlConfigurationPath, "tests.yaml"));
			if (File.Exists(localYamlFile))
				return new YamlConfiguration(localYamlFile);

			var defaultYamlFile = Path.GetFullPath(Path.Combine(yamlConfigurationPath, "tests.default.yaml"));
			if (File.Exists(defaultYamlFile))
				return new YamlConfiguration(defaultYamlFile);

			throw new Exception($"Tried to load a yaml file from {yamlConfigurationPath} but it does not exist : pwd:{directoryInfo.FullName}");
		}

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("NEST_NUMBER_OF_CONNECTIONS"), out int x)
			? x
			: ConnectionConfiguration.DefaultConnectionLimit;

		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();

		private static ConnectionSettings DefaultSettings(ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.PrettyJson()
			.InferMappingFor<Project>(map=>map
				.IndexName(DefaultSeeder.ProjectsIndex)
				.IdProperty(p => p.Name)
				.RelationName("project")
				.TypeName("doc")
			)
			.InferMappingFor<CommitActivity>(map => map
				.IndexName(DefaultSeeder.ProjectsIndex)
				.RelationName("commits")
				.TypeName("doc")
			)
			.InferMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.Rename(p => p.OnlineHandle, "nickname")
			)
			.InferMappingFor<ProjectPercolation>(map => map
				.IndexName("queries")
				.TypeName(PercolatorType)
			)
			.InferMappingFor<Metric>(map => map
				.IndexName("server-metrics")
				.TypeName("metric")
			)
			.ConnectionLimit(ConnectionLimitDefault)
			//.Proxy(new Uri("http://127.0.0.1:8888"), "", "")
			//.EnableTcpKeepAlive(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2))
			//.PrettyJson()
			//TODO make this random
			//.EnableHttpCompression()
			.OnRequestCompleted(r =>
			{
				if (!r.DeprecationWarnings.Any()) return;
				var q = r.Uri.Query;
				//hack to prevent the deprecation warnings from the deprecation response test to be reported
				if (!string.IsNullOrWhiteSpace(q) && q.Contains("routing=ignoredefaultcompletedhandler")) return;
				foreach (var d in r.DeprecationWarnings) SeenDeprecations.Add(d);
			})
			.OnRequestDataCreated(data => data.Headers.Add("TestMethod", ExpensiveTestNameForIntegrationTests()));

		public static string PercolatorType => Configuration.ElasticsearchVersion <= ElasticsearchVersion.GetOrAdd("5.0.0-alpha1")
			? ".percolator"
			: "query";

		public static bool VersionSatisfiedBy(string range, ElasticsearchVersion version)
		{
			var versionRange = new SemVer.Range(range);
			var satisfied = versionRange.IsSatisfied(version.Version);
			if (!version.IsSnapshot || satisfied) return satisfied;
			//Semver can only match snapshot version with ranges on the same major and minor
			//anything else fails but we want to know e.g 2.4.5-SNAPSHOT satisfied by <5.0.0;
			var wholeVersion = $"{version.Major}.{version.Minor}.{version.Patch}";
			return versionRange.IsSatisfied(wholeVersion);

		}

		public static bool VersionUnderTestSatisfiedBy(string range) =>
			VersionSatisfiedBy(range, TestClient.Configuration.ElasticsearchVersion);

		public static ConnectionSettings CreateSettings(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceInMemory = false,
			bool forceSsl = false,
			Func<Uri, IConnectionPool> createPool = null,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null
		)
		{
			createPool = createPool ?? (u => new SingleNodeConnectionPool(u));

			var connectionPool = createPool(CreateUri(port, forceSsl));
			var connection = CreateConnection(forceInMemory: forceInMemory);
			var s = new ConnectionSettings(connectionPool, connection, sourceSerializerFactory, propertyMappingProvider);

			var defaultSettings = DefaultSettings(s);
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static IElasticClient GetInMemoryClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: true).EnableDebugMode());

		public static IElasticClient GetInMemoryClientWithSourceSerializer(
			Func<ConnectionSettings, ConnectionSettings> modifySettings,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null) =>
			new ElasticClient(
				CreateSettings(modifySettings, forceInMemory: true, sourceSerializerFactory: sourceSerializerFactory,
					propertyMappingProvider: propertyMappingProvider)
				);

		public static IElasticClient GetClient(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceSsl = false,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null) =>
			new ElasticClient(
				CreateSettings(modifySettings, port, forceInMemory: false, forceSsl: forceSsl, sourceSerializerFactory: sourceSerializerFactory)
			);

		public static IElasticClient GetClient(
			Func<Uri, IConnectionPool> createPool,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,

			int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: false, createPool: createPool));

		public static IConnection CreateConnection(ConnectionSettings settings = null, bool forceInMemory = false) =>
			Configuration.RunIntegrationTests && !forceInMemory
				? ((IConnection) new HttpConnection())
				: new InMemoryConnection();

		public static IElasticClient GetFixedReturnClient(
			object response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = "application/json",
			Exception exception = null)
		{
			var serializer = Default.RequestResponseSerializer;
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
			if (!Configuration.RunIntegrationTests) return "ignore";

#if DOTNETCORE
			return "UNKNOWN";
#else
			var st = new StackTrace();
			var types = GetTypes(st);
			var name = types
				.LastOrDefault(type => type.FullName.StartsWith("Tests.") && !type.FullName.StartsWith("Tests.Framework."));
			return name?.FullName ?? string.Join(": ", types.Select(n => n.Name));
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
