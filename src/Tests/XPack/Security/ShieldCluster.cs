using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Security
{

	[CollectionDefinition(IntegrationContext.Shield)]
	public class ShieldCluster : ClusterBase, ICollectionFixture<ShieldCluster>, IClassFixture<EndpointUsage>
	{
		protected override bool EnableShield => true;
	}
}
