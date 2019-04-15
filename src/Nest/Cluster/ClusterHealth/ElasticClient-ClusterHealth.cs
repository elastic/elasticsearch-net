using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster health operation</param>
		ClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		ClusterHealthResponse ClusterHealth(IClusterHealthRequest request);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		Task<ClusterHealthResponse> ClusterHealthAsync(
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		Task<ClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public ClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null) =>
			ClusterHealth(selector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public ClusterHealthResponse ClusterHealth(IClusterHealthRequest request) =>
			DoRequest<IClusterHealthRequest, ClusterHealthResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public Task<ClusterHealthResponse> ClusterHealthAsync(
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		) => ClusterHealthAsync(selector.InvokeOrDefault(new ClusterHealthDescriptor()), ct);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public Task<ClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClusterHealthRequest, ClusterHealthResponse, ClusterHealthResponse>(request, request.RequestParameters, ct);
	}
}
