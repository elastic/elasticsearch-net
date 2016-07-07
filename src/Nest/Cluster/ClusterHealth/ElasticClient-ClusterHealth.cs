using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster health operation</param>
		IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc/>
		IClusterHealthResponse ClusterHealth(IClusterHealthRequest request);

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null) =>
			this.ClusterHealth(selector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(IClusterHealthRequest request) =>
			this.Dispatcher.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatch<ClusterHealthResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClusterHealthAsync(selector.InvokeOrDefault(new ClusterHealthDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse, IClusterHealthResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.ClusterHealthDispatchAsync<ClusterHealthResponse>(p, c)
			);
	}
}
