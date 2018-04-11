using Elastic.Managed.Ephemeral.Plugins;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster()
		{
			this.Plugins.Add(ElasticsearchPlugin.MapperMurmur3);
		}

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
