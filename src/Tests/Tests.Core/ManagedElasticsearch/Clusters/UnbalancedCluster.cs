using Nest;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	//TODO does this need a whole separate cluster?
	public class UnbalancedCluster : ClientTestClusterBase
	{
		protected override void SeedCluster() =>
			new DefaultSeeder(this.Client, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 })
				.SeedNode();
	}
}
