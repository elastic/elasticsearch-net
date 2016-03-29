using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(IntegrationContext.OwnIndex)]
	public class OwnIndexCluster : ClusterBase, ICollectionFixture<OwnIndexCluster>, IClassFixture<EndpointUsage>
	{
	}
}