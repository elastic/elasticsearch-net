using Tests.Core.ManagedElasticsearch.NodeSeeders;
using static Elastic.Stack.ArtifactsApi.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(MapperMurmur3) { }

		protected override void SeedNode() => new DefaultSeeder(Client).SeedNode();
	}
}
