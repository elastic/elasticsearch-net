using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Profiling.Framework
{
	public class ProfilingCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
