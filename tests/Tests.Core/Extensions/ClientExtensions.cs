//using Elastic.Clients.Elasticsearch.Cluster;

namespace Tests.Core.Extensions
{
	public static class ClientExtensions
	{
		//public static HealthResponse WaitForSecurityIndices(this IElasticsearchClient client) =>
		//	client.Cluster.Health(
		//		new HealthRequest(".security-*")
		//		{
		//			WaitForStatus = WaitForStatus.Green /*, ExpandWildcards = ExpandWildcards.All*/
		//		});

		//public static async Task<HealthResponse> WaitForSecurityIndicesAsync(this IElasticsearchClient client) =>
		//	await client.Cluster.HealthAsync(
		//		new HealthRequest(".security-*")
		//		{
		//			WaitForStatus = WaitForStatus.Green,
		//			//ExpandWildcards = ExpandWildcards.All
		//		}).ConfigureAwait(false);
	}
}
