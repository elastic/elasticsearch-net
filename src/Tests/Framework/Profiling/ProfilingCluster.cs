using Tests.Framework.Integration;

namespace Tests.Framework.Profiling
{
	public class ProfilingCluster : ClusterBase
	{
		protected override void AfterNodeStarts()
		{
			var seeder = new Seeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
