using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using static Elastic.Managed.Ephemeral.Plugins.ElasticsearchPlugin;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(MapperMurmur3) { }

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
