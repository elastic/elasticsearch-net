using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.MapperMurmer3)]
	public class ReadOnlyCluster : ClusterBase
	{
		protected override void SeedNode() => new DefaultSeeder(this.Node).SeedNode();
	}
}
