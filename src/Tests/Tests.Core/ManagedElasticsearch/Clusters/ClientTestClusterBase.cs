using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit;
using Nest;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Tasks;
using Tests.Domain.Extensions;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>, INestTestCluster
	{
		public ClientTestClusterBase() : this(new ClientTestClusterConfiguration()) { }

		public ClientTestClusterBase(params ElasticsearchPlugin[] plugins) : this(new ClientTestClusterConfiguration(plugins)) { }

		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }

		public IElasticClient Client => this.GetOrAddClient(s => ConnectionSettings(s.ApplyDomainSettings()));

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s;

		// TODO don't think this override is needed anymore
		public override ICollection<Uri> NodesUris(string hostName = "localhost")
		{
			var host = TestConnectionSettings.RunningFiddler ? "ipv4.fiddler" : hostName;
			return base.NodesUris(host);
		}
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
			ShowElasticsearchOutputAfterStarted = true; //this.TestConfiguration.ShowElasticsearchOutputAfterStarted;

			CacheEsHomeInstallation = true;

			Add(AttributeKey("testingcluster"), "true");
			Add(AttributeKey("gateway"), "true");
			Add("search.remote.connect", "true", ">=5.3.0");

			Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1");

			Add($"script.inline", "true", "<6.0.0");
			Add($"script.stored", "true", ">5.0.0-alpha1 <6.0.0");
			Add($"script.indexed", "true", "<5.0.0-alpha1");
			Add($"script.allowed_types", "inline,stored", ">=6.0.0");

			AdditionalBeforeNodeStartedTasks.Add(new WriteAnalysisFiles());
		}

		public string AnalysisFolder => Path.Combine(FileSystem.ConfigPath, "analysis");
		public ITestConfiguration TestConfiguration { get; }
	}
}
