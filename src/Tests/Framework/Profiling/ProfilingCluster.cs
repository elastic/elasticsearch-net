using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.Profiling
{
	public class ProfilingCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(this.Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
