using System.Threading.Tasks;
using Nest;
using Nest.Cluster.Health;
using Nest.Core;

namespace Tests.Core.Extensions
{
	public static class ClientExtensions
	{
		public static ClusterHealthResponse WaitForSecurityIndices(this IElasticClient client) =>
			client.Cluster.ClusterHealth(
				new ClusterHealthRequest(".security-*")
				{
					WaitForStatus = WaitForStatus.Green /*, ExpandWildcards = ExpandWildcards.All*/
				});

		public static async Task<ClusterHealthResponse> WaitForSecurityIndicesAsync(this IElasticClient client) =>
			await client.Cluster.ClusterHealthAsync(
				new ClusterHealthRequest(".security-*")
				{
					WaitForStatus = WaitForStatus.Green,
					//ExpandWildcards = ExpandWildcards.All
				}).ConfigureAwait(false);
	}
}
