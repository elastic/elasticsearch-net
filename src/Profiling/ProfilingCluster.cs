using Tests.Framework.Integration;

namespace Profiling
{
    public class ProfilingCluster : ClusterBase
    {
        public override void Boostrap()
        {
            var seeder = new Seeder(this.Node.Port);
            seeder.DeleteIndices();
            seeder.CreateIndices();
        }
    }
}