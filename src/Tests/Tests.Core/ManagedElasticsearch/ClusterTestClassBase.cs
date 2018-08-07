using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Core.ManagedElasticsearch
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
			this.Cluster.ClusterConfiguration.CacheEsHomeInstallation = true;
		}
	}
}
