using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster health operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterHealthResponse ClusterHealth(this IElasticClient client,
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null
		)
			=> client.Cluster.Health(Indices.All, selector);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterHealthResponse ClusterHealth(this IElasticClient client, IClusterHealthRequest request)
			=> client.Cluster.Health(request);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client,
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.HealthAsync(Indices.All, selector, ct);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client, IClusterHealthRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.HealthAsync(request, ct);
	}
}
