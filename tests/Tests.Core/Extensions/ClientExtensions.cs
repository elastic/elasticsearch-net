using Elasticsearch.Net;
using Nest;

namespace Tests.Core.Extensions
{
	public static class ClientExtensions
	{
		public static ClusterHealthResponse WaitForSecurityIndices(this IElasticClient client) =>
			client.Cluster.Health(new ClusterHealthRequest(".security-*") { WaitForStatus = WaitForStatus.Green });
	}
}
