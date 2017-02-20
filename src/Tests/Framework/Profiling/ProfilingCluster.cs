using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.Profiling
{
	public class ProfilingCluster : ClusterBase
	{
		protected override void SeedNode()
		{
			var seeder = new DefaultSeeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
