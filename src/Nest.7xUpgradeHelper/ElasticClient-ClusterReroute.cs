using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands.
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled,
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterRerouteResponse ClusterReroute(this IElasticClient client,
			Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector
		)
			=> client.Cluster.Reroute(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client,
			Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		)
			=> client.Cluster.RerouteAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterRerouteResponse ClusterReroute(this IElasticClient client, IClusterRerouteRequest request)
			=> client.Cluster.Reroute(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client, IClusterRerouteRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.RerouteAsync(request, ct);
	}
}
