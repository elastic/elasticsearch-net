using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Core.ManagedElasticsearch
{
	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		protected ClusterTestClassBase(TCluster cluster)
		{
			Cluster = cluster;
			Cluster.ClusterConfiguration.ShowElasticsearchOutputAfterStarted = false;
			Cluster.ClusterConfiguration.CacheEsHomeInstallation = true;
		}

		public IElasticClient Client => Cluster.Client;
		public TCluster Cluster { get; }
	}
}
