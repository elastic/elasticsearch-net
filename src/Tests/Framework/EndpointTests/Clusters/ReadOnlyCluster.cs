using System;
using Xunit;

namespace Tests.Framework.Integration
{
	public class ReadOnlyCluster : ClusterBase
	{
		public override void Bootstrap() => new Seeder(this.Node).SeedNode();
	}
}
