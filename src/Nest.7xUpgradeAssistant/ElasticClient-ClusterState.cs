using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.State(), please update this usage.")]
		public static ClusterStateResponse ClusterState(this IElasticClient client, Func<ClusterStateDescriptor, IClusterStateRequest> selector = null
		)
			=> client.Cluster.State(Indices.All, selector);

		[Obsolete("Moved to client.Cluster.State(), please update this usage.")]
		public static ClusterStateResponse ClusterState(this IElasticClient client, IClusterStateRequest request)
			=> client.Cluster.State(request);

		[Obsolete("Moved to client.Cluster.StateAsync(), please update this usage.")]
		public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client,
			Func<ClusterStateDescriptor, IClusterStateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.StateAsync(Indices.All, selector, ct);

		[Obsolete("Moved to client.Cluster.StateAsync(), please update this usage.")]
		public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client, IClusterStateRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.StateAsync(request, ct);
	}
}
