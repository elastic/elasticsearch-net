using System;
using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class ReadOnlyCluster : ClusterBase, ICollectionFixture<ReadOnlyCluster>, IClassFixture<EndpointUsage>
	{
		public override void Boostrap() => new Seeder(this.Node).SeedNode();
	}
}
