using System;
using Nest;

namespace Tests.Framework.Integration
{
	public class UnbalancedCluster : ReadOnlyCluster
	{
		public override void Bootstrap() =>
			new Seeder(this.Node, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 }).SeedNode();
	}
}
