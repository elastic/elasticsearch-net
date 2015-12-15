using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(IntegrationContext.Indexing)]
	public class IndexingCluster : ClusterBase, ICollectionFixture<IndexingCluster>, IClassFixture<EndpointUsage>
	{
		public override void Boostrap() { }
	}
}
