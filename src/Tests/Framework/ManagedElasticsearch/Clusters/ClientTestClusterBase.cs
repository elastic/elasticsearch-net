using System;
using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>, INestTestCluster
	{
		public ClientTestClusterBase() : this(new ClientTestClusterConfiguration()) { }

		public ClientTestClusterBase(params ElasticsearchPlugin[] plugins) : this(new ClientTestClusterConfiguration(plugins: plugins)) { }

		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration)
		{
			this.ClusterConfiguration.AdditionalInstallationTasks.Add(new EnsureElasticsearchBatWorksAcrossDrives());
		}

		public IElasticClient Client => this.GetOrAddClient(ConnectionSettings);

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s;
	}

	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ITestConfiguration TestConfiguration { get; }

		public ClientTestClusterConfiguration(params ElasticsearchPlugin[] plugins) : this(numberOfNodes: 1, plugins: plugins) { }

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1, params ElasticsearchPlugin[] plugins)
			: base(TestClient.Configuration.ElasticsearchVersion, features, new ElasticsearchPlugins(plugins), numberOfNodes)
		{
			this.TestConfiguration = TestClient.Configuration;
			this.ShowElasticsearchOutputAfterStarted = false;

			this.Add(this.AttributeKey("testingcluster"), "true");
			this.Add(this.AttributeKey("gateway"), "true");
			this.Add("search.remote.connect", "true");

			this.Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			this.Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1");

			this.Add($"script.inline", "true", "<5.5.0");
			this.Add($"script.stored", "true", ">5.0.0-alpha1 <5.5.0");
			this.Add($"script.indexed", "true", "<5.0.0-alpha1");
			this.Add($"script.allowed_types", "inline,stored", ">=5.5.0");

			this.AdditionalInstallationTasks.Add(new WriteAnalysisFiles());
		}

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		public string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";

		public string AnalysisFolder => Path.Combine(this.FileSystem.ConfigPath, "analysis");
	}
}
