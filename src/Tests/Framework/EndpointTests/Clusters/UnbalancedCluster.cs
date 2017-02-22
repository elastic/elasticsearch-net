using System;
using Nest_5_2_0;

namespace Tests.Framework.Integration
{
	public class UnbalancedCluster : ReadOnlyCluster
	{
		public override void Bootstrap() =>
			new Seeder(this.Node, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 }).SeedNode();
	}
}
