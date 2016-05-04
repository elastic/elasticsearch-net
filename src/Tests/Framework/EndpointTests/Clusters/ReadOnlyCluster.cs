using System;
using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(TypeOfCluster.ReadOnly)]
	public class ReadOnlyCluster : ClusterBase, ICollectionFixture<ReadOnlyCluster>, IClassFixture<EndpointUsage>
	{
		protected override void Boostrap() => new Seeder(this.Node).SeedNode();
	}
}
