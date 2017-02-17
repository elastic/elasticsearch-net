using System;
using Nest;

namespace Tests.Framework.Integration
{
	public class UnbalancedCluster : ReadOnlyCluster
	{
		protected override void AfterNodeStarts() =>
			new Seeder(this.Node, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 }).SeedNode();
	}
}
