using Nest;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class UnbalancedCluster : ReadOnlyCluster
	{
		protected override void SeedNode() =>
			new DefaultSeeder(this.Node, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 })
				.SeedNode();
	}
}
