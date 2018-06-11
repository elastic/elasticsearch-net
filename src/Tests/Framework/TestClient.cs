using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Elastic.Managed.Configuration;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.MockData;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Framework
{
	public class IntegrationOnlyAttribute : SkipTestAttributeBase
	{
		public override bool Skip => TestClient.Configuration.RunUnitTests;
		public override string Reason { get; } = "Inherited unit tests are ignored on this integration test class";
	}
	public class NeedsTypedKeysAttribute : SkipTestAttributeBase
	{
		public override bool Skip => !TestClient.Configuration.Random.TypedKeys;
		public override string Reason { get; } = "Random Configuration dictates no typed keys but this tests relies on it being set";
	}
	public class ProjectReferenceOnlyAttribute : SkipTestAttributeBase
	{
		public override bool Skip => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TestPackageVersion"));
		public override string Reason { get; } = "This test can only be run if client dependencies are project references";
	}
	public class SkipOnTeamCityAttribute : SkipTestAttributeBase
	{
		public override bool Skip => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";
	}
	public static class TestClient
	{
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();
		public static readonly ITestConfiguration Configuration = ConfigurationLoader.LoadConfiguration();

		public static ConnectionSettings GlobalDefaultSettings = CreateSettings();
		public static IElasticClient Default = new ElasticClient(GlobalDefaultSettings);
		public static IElasticClient DefaultInMemoryClient = GetInMemoryClient();

		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();

		public static Uri CreateUri(int port = 9200, bool forceSsl = false) =>
			new UriBuilder(forceSsl ? "https" : "http", Host, port).Uri;

		public static string DefaultHost => "localhost";
		public static string Host => (RunningFiddler) ? "ipv4.fiddler" : DefaultHost;

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("NEST_NUMBER_OF_CONNECTIONS"), out int x)
			? x
			: ConnectionConfiguration.DefaultConnectionLimit;

		private static ConnectionSettings DefaultSettings(ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.PrettyJson()
			.InferMappingFor<Project>(ProjectMapping)
			.InferMappingFor<Ranges>(RangesMapping)
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
			.InferMappingFor<Metric>(map => map
				.IndexName("server-metrics")
				.TypeName("metric")
			)
			.InferMappingFor<Shape>(map => map
				.IndexName("shapes")
				.TypeName("doc")
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

		private static IClrTypeMapping<Project> ProjectMapping(ClrTypeMappingDescriptor<Project> m)
		{
			m.IndexName("project").IdProperty(p => p.Name);
			//*_range type only available since 5.2.0 so we ignore them when running integration tests
			if (TestClient.Configuration.ElasticsearchVersion.InRange("<5.2.0") && Configuration.RunIntegrationTests)
				m.Ignore(p => p.Ranges);
			return m;
		}

		private static IClrTypeMapping<Ranges> RangesMapping(ClrTypeMappingDescriptor<Ranges> m)
		{
			//ip_range type only available since 5.5.0 so we ignore them when running integration tests
			if (TestClient.Configuration.ElasticsearchVersion.InRange("<5.5.0") && Configuration.RunIntegrationTests)
				m.Ignore(p => p.Ips);
			return m;
		}

		public static string PercolatorType => Configuration.ElasticsearchVersion <= "5.0.0-alpha1"
			? ".percolator"
			: "query";

		public static ConnectionSettings CreateSettings(Func<ConnectionSettings, ConnectionSettings> modifySettings, IConnection connection, IConnectionPool connectionPool)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			var defaultSettings = DefaultSettings(new ConnectionSettings(connectionPool, connection));
#pragma warning restore CS0618 // Type or member is obsolete
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static ConnectionSettings CreateSettings(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceInMemory = false,
			bool forceSsl = false,
			Func<Uri, IConnectionPool> createPool = null,
			Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory = null
		)
		{
			createPool = createPool ?? (u => new SingleNodeConnectionPool(u));
#pragma warning disable CS0618 // Type or member is obsolete
			var defaultSettings = DefaultSettings(new ConnectionSettings(createPool(CreateUri(port, forceSsl)),
				CreateConnection(forceInMemory: forceInMemory), serializerFactory));
#pragma warning restore CS0618 // Type or member is obsolete
			var settings = modifySettings != null ? modifySettings(defaultSettings) : defaultSettings;
			return settings;
		}

		public static IElasticClient GetInMemoryClient(Func<ConnectionSettings, ConnectionSettings> modifySettings = null, int port = 9200) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: true));

		public static IElasticClient GetInMemoryClientWithSerializerFactory(
			Func<ConnectionSettings, ConnectionSettings> modifySettings,
			Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory) =>
			new ElasticClient(CreateSettings(modifySettings, forceInMemory: true, serializerFactory: serializerFactory));

		public static IElasticClient GetClient(
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			int port = 9200,
			bool forceSsl = false) =>
			new ElasticClient(CreateSettings(modifySettings, port, forceInMemory: false, forceSsl: forceSsl));

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
		public static IElasticClient GetFixedStringResponseClient(
			string response,
			int statusCode = 200,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null,
			string contentType = "application/json",
			Exception exception = null)
		{
			var fixedResult = Encoding.UTF8.GetBytes(response);

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
