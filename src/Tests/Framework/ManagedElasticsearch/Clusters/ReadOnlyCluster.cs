using Elastic.Managed.Ephemeral.Plugins;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.MapperMurmer3)]
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
