using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.Reroute(), please update this usage.")]
		public static ClusterRerouteResponse ClusterReroute(this IElasticClient client,
			Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector
		)
			=> client.Cluster.Reroute(selector);

		[Obsolete("Moved to client.Cluster.RerouteAsync(), please update this usage.")]
		public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client,
			Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		)
			=> client.Cluster.RerouteAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.Reroute(), please update this usage.")]
		public static ClusterRerouteResponse ClusterReroute(this IElasticClient client, IClusterRerouteRequest request)
			=> client.Cluster.Reroute(request);

		[Obsolete("Moved to client.Cluster.RerouteAsync(), please update this usage.")]
		public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client, IClusterRerouteRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.RerouteAsync(request, ct);
	}
}
