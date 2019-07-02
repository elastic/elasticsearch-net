using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.Health(), please update this usage.")]
		public static ClusterHealthResponse ClusterHealth(this IElasticClient client,
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null
		)
			=> client.Cluster.Health(Indices.All, selector);

		[Obsolete("Moved to client.Cluster.Health(), please update this usage.")]
		public static ClusterHealthResponse ClusterHealth(this IElasticClient client, IClusterHealthRequest request)
			=> client.Cluster.Health(request);

		[Obsolete("Moved to client.Cluster.HealthAsync(), please update this usage.")]
		public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client,
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.HealthAsync(Indices.All, selector, ct);

		[Obsolete("Moved to client.Cluster.HealthAsync(), please update this usage.")]
		public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client, IClusterHealthRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.HealthAsync(request, ct);
	}
}
