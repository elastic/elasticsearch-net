using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the cluster state operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterStateResponse ClusterState(this IElasticClient client, Func<ClusterStateDescriptor, IClusterStateRequest> selector = null
		)
			=> client.Cluster.State(Indices.All, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterStateResponse ClusterState(this IElasticClient client, IClusterStateRequest request)
			=> client.Cluster.State(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client,
			Func<ClusterStateDescriptor, IClusterStateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.StateAsync(Indices.All, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client, IClusterStateRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.StateAsync(request, ct);
	}
}
