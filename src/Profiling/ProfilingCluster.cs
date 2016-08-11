using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Profiling
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
