using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static ClusterHealthResponse ClusterHealth(this IElasticClient client,Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public static ClusterHealthResponse ClusterHealth(this IElasticClient client,IClusterHealthRequest request);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client,
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public static Task<ClusterHealthResponse> ClusterHealthAsync(this IElasticClient client,IClusterHealthRequest request, CancellationToken ct = default);
	}

}
