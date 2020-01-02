using Tests.Core.ManagedElasticsearch.NodeSeeders;
using static Elastic.Stack.Artifacts.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(MapperMurmur3) { }

		protected override void SeedCluster() => new DefaultSeeder(Client).SeedNode();
	}
}
