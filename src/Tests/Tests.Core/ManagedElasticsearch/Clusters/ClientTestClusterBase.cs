using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit;
using Nest;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Tasks;
using Tests.Domain.Extensions;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public abstract class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>, INestTestCluster
	{
		public ClientTestClusterBase() : this(new ClientTestClusterConfiguration()) { }

		public ClientTestClusterBase(params ElasticsearchPlugin[] plugins) : this(new ClientTestClusterConfiguration(plugins)) { }

		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }

		public IElasticClient Client => this.GetOrAddClient(s => ConnectionSettings(s.ApplyDomainSettings()));

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s;
	}

	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ClientTestClusterConfiguration(params ElasticsearchPlugin[] plugins) : this(numberOfNodes: 1, plugins: plugins) { }

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1,
			params ElasticsearchPlugin[] plugins
		)
			: base(TestClient.Configuration.ElasticsearchVersion, features, new ElasticsearchPlugins(plugins), numberOfNodes)
		{
			TestConfiguration = TestClient.Configuration;
			ShowElasticsearchOutputAfterStarted = TestConfiguration.ShowElasticsearchOutputAfterStarted;
			HttpFiddlerAware = true;

			CacheEsHomeInstallation = true;

			Add(AttributeKey("testingcluster"), "true");
			Add(AttributeKey("gateway"), "true");
			Add("search.remote.connect", "true");

			Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1");

			Add($"script.inline", "true", "<5.5.0");
			Add($"script.stored", "true", ">5.0.0-alpha1 <5.5.0");
			Add($"script.indexed", "true", "<5.0.0-alpha1");
			Add($"script.allowed_types", "inline,stored", ">=5.5.0");

			AdditionalBeforeNodeStartedTasks.Add(new WriteAnalysisFiles());
		}

		public string AnalysisFolder => Path.Combine(FileSystem.ConfigPath, "analysis");
		public TestConfigurationBase TestConfiguration { get; }
	}
}
