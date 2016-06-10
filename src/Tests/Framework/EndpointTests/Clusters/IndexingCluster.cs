using Xunit;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(TypeOfCluster.Indexing)]
	[RequiresPlugin(ElasticsearchPlugin.MapperAttachments, ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.IngestAttachment)]
	public class IndexingCluster : ClusterBase, ICollectionFixture<IndexingCluster>, IClassFixture<EndpointUsage>
	{
		protected override void Boostrap() { }
	}
}
