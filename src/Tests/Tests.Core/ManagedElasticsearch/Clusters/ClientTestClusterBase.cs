using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

		public ClientTestClusterBase(params ElasticsearchPlugin[] plugins) : this(new ClientTestClusterConfiguration(plugins: plugins)) { }

		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }

		public IElasticClient Client => this.GetOrAddClient(s=> this.ConnectionSettings(ConnectionSettingsExtensions.ApplyDomainSettings(s)));

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s;

		// TODO don't think this override is needed anymore
		public override ICollection<Uri> NodesUris(string hostName = "localhost")
		{
			var host = (TestConnectionSettings.RunningFiddler) ? "ipv4.fiddler" : hostName;
			return base.NodesUris(host);
		}
	}

	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ITestConfiguration TestConfiguration { get; }

		public ClientTestClusterConfiguration(params ElasticsearchPlugin[] plugins) : this(numberOfNodes: 1, plugins: plugins) { }

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1, params ElasticsearchPlugin[] plugins)
			: base(TestClient.Configuration.ElasticsearchVersion, features, new ElasticsearchPlugins(plugins), numberOfNodes)
		{
			this.TestConfiguration = TestClient.Configuration;
			this.ShowElasticsearchOutputAfterStarted = this.TestConfiguration.ShowElasticsearchOutputAfterStarted;

			this.CacheEsHomeInstallation = true;
			this.TrialMode = XPackTrialMode.Trial;

			this.Add(this.AttributeKey("testingcluster"), "true");
			this.Add(this.AttributeKey("gateway"), "true");
			this.Add("search.remote.connect", "true", ">=5.3.0");

			this.Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			this.Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1");

			this.Add($"script.inline", "true", "<6.0.0");
			this.Add($"script.stored", "true", ">5.0.0-alpha1 <6.0.0");
			this.Add($"script.indexed", "true", "<5.0.0-alpha1");
			this.Add($"script.allowed_types", "inline,stored", ">=6.0.0");

			this.AdditionalBeforeNodeStartedTasks.Add(new WriteAnalysisFiles());
		}

		public string AnalysisFolder => Path.Combine(this.FileSystem.ConfigPath, "analysis");
	}
}
