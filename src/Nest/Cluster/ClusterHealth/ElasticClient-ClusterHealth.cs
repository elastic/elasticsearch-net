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
		IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		IClusterHealthResponse ClusterHealth(IClusterHealthRequest request);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		Task<IClusterHealthResponse> ClusterHealthAsync(
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null) =>
			ClusterHealth(selector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public IClusterHealthResponse ClusterHealth(IClusterHealthRequest request) =>
			DoRequest<IClusterHealthRequest, ClusterHealthResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public Task<IClusterHealthResponse> ClusterHealthAsync(
			Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null,
			CancellationToken ct = default
		) => ClusterHealthAsync(selector.InvokeOrDefault(new ClusterHealthDescriptor()), ct);

		/// <inheritdoc cref="ClusterHealth(System.Func{Nest.ClusterHealthDescriptor,Nest.IClusterHealthRequest})" />
		public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClusterHealthRequest, IClusterHealthResponse, ClusterHealthResponse>(request, request.RequestParameters, ct);
	}
}
