using System.IO;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Plugins;
using Elastic.Elasticsearch.Xunit;
using Elastic.Stack.ArtifactsApi.Products;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Cluster;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Tasks;
using Tests.Domain.Extensions;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public abstract class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>, ITestCluster
	{
		protected ClientTestClusterBase() : this(new ClientTestClusterConfiguration()) { }

		protected ClientTestClusterBase(params ElasticsearchPlugin[] plugins) : this(
			new ClientTestClusterConfiguration(plugins))
		{
		}

		protected ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }

		public IElasticClient Client => this.GetOrAddClient(s => ConnectionSettings(s.ApplyDomainSettings()));

		protected virtual ElasticsearchClientSettings ConnectionSettings(ElasticsearchClientSettings s) => s;

		protected sealed override void SeedCluster()
		{
			Client.Cluster.Health(new ClusterHealthRequest { WaitForStatus = HealthStatus.Green });
			SeedNode();
			Client.Cluster.Health(new ClusterHealthRequest { WaitForStatus = HealthStatus.Green });
		}

		protected virtual void SeedNode() { }
	}

	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ClientTestClusterConfiguration(params ElasticsearchPlugin[] plugins) : this(numberOfNodes: 1,
			plugins: plugins)
		{
		}

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1,
			params ElasticsearchPlugin[] plugins
		)
			: base(TestClient.Configuration.ElasticsearchVersion, features, new ElasticsearchPlugins(plugins),
				numberOfNodes)
		{
			TestConfiguration = TestClient.Configuration;
			ShowElasticsearchOutputAfterStarted = TestConfiguration.ShowElasticsearchOutputAfterStarted;
			HttpFiddlerAware = true;

			CacheEsHomeInstallation = true;

			Add(AttributeKey("testingcluster"), "true");
			Add(AttributeKey("gateway"), "true");
			Add("search.remote.connect", "true", "<8.0.0");
			//Add("node.remote_cluster_client", "true", ">=8.0.0-SNAPSHOT");

			//Add("cluster.routing.allocation.disk.watermark.high", "95%");
			Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1 <7.9.0-SNAPSHOT");
			Add($"script.disable_max_compilations_rate", "true", ">=7.9.0-SNAPSHOT");

			Add($"script.inline", "true", "<5.5.0");
			Add($"script.stored", "true", ">5.0.0-alpha1 <5.5.0");
			Add($"script.indexed", "true", "<5.0.0-alpha1");
			Add($"script.allowed_types", "inline,stored", ">=5.5.0");

			Add($"xpack.security.http.ssl.enabled", "false", ">=7.99.99");

			AdditionalBeforeNodeStartedTasks.Add(new WriteAnalysisFiles());
		}

		public string AnalysisFolder => Path.Combine(FileSystem.ConfigPath, "analysis");
		public TestConfigurationBase TestConfiguration { get; }
	}
}
