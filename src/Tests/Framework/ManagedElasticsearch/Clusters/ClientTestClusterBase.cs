using System;
using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>, INestTestCluster
	{
		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }
		public ClientTestClusterBase() : base(new ClientTestClusterConfiguration()) { }

		public IElasticClient Client => this.GetOrAddClient(ConnectionSettings);

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s;
	}

	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ITestConfiguration TestConfiguration { get; }

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1)
			: base(TestClient.Configuration.ElasticsearchVersion, features, numberOfNodes)
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
