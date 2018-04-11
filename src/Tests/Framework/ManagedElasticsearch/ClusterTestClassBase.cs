using Elastic.Managed.Ephemeral;
using Elastic.Xunit.Sdk;
using Nest;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework.ManagedElasticsearch
{
	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		public TCluster Cluster { get; }
		public IElasticClient Client => this.Cluster.Client;

		protected ClusterTestClassBase(TCluster cluster)
		{
			this.Cluster = cluster;
			this.Cluster.ClusterConfiguration.ShowElasticsearchOutputAfterStarted = false;
		}
	}
}
