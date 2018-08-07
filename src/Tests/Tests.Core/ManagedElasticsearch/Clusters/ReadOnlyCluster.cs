using Elastic.Managed.Ephemeral.Plugins;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(ElasticsearchPlugin.MapperMurmur3) { }

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
