using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Profiling
{
	public class ProfilingCluster : ClusterBase
	{
		protected override void Boostrap()
		{
			var seeder = new Seeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}
}
