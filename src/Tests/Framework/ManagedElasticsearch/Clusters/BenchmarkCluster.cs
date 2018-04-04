using Elastic.Xunit;
using Elastic.Xunit.XunitPlumbing;
using Elastic.Managed.Ephemeral;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1)
			: base(TestClient.Configuration.ElasticsearchVersion, features, numberOfNodes)
		{
		}

		//TODO ugly
		public int MaxConcurrencySetter { get; set; }
		public override int MaxConcurrency { get => MaxConcurrencySetter; }
	}

	public class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>
	{
		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }
		public ClientTestClusterBase() : base(new ClientTestClusterConfiguration()) { }
	}


	public class BenchmarkCluster : ClientTestClusterBase
	{
	}
}
