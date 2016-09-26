using Tests.Framework.Integration;

namespace Tests.Framework.Profiling
{
	public class ProfilingCluster : ClusterBase
	{
		public override void Bootstrap()
		{
			var seeder = new Seeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
