using System;
using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class ReadOnlyCluster : ClusterBase, ICollectionFixture<ReadOnlyCluster>, IClassFixture<EndpointUsage>
	{
		protected override bool DoNotSpawnIfAlreadyRunning => true;
		public override void Boostrap() => new Seeder(this.Node.Port).SeedNode();
	}
}
