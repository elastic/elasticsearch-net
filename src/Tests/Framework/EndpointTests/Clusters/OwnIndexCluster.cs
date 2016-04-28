using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(TypeOfCluster.OwnIndex)]
	[RequiresPlugin(ElasticsearchPlugin.DeleteByQuery)]
	public class OwnIndexCluster : ClusterBase, ICollectionFixture<OwnIndexCluster>, IClassFixture<EndpointUsage>
	{
	}
}
