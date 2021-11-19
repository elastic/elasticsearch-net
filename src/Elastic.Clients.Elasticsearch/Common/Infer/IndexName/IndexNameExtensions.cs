namespace Elastic.Clients.Elasticsearch
{
	//public static class IndexNameExtensions
	//{
	//	public static string? Resolve(this IndexName? marker, IElasticsearchClientSettings elasticsearchClientSettings)
	//	{
	//		if (marker == null)
	//			return null;

	//		elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));

	//		return marker.Type == null
	//			? marker.Name
	//			: new IndexNameResolver(elasticsearchClientSettings).Resolve(marker.Type);
	//	}
	//}
}
