using System.Threading.Tasks;
using Nest;
using Nest.Core;

namespace Tests.Core.Extensions
{
	public static class ClientExtensions
	{
		public static ClusterHealthResponse WaitForSecurityIndices(this IElasticClient client) =>
			client.Cluster.Health(
				new ClusterHealthRequest(".security-*")
				{
					WaitForStatus = WaitForStatus.Green /*, ExpandWildcards = ExpandWildcards.All*/
				});

		public static async Task<ClusterHealthResponse> WaitForSecurityIndicesAsync(this IElasticClient client) =>
			await client.Cluster.HealthAsync(
				new ClusterHealthRequest(".security-*")
				{
					WaitForStatus = WaitForStatus.Green,
					//ExpandWildcards = ExpandWildcards.All
				}).ConfigureAwait(false);
	}
}
