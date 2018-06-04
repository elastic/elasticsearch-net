using Nest;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	//TODO does this need a whole separate cluster?
	public class UnbalancedCluster : ClientTestClusterBase
	{
		protected override void SeedCluster() =>
			new DefaultSeeder(this.Client, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 })
				.SeedNode();
	}
}
