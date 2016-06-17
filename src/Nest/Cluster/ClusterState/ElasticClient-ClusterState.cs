using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the cluster state operation</param>
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null);

		/// <inheritdoc/>
		IClusterStateResponse ClusterState(IClusterStateRequest request);

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null) =>
			this.ClusterState(selector.InvokeOrDefault(new ClusterStateDescriptor()));

		/// <inheritdoc/>
		public IClusterStateResponse ClusterState(IClusterStateRequest request) =>
			this.Dispatcher.Dispatch<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClusterStateDispatch<ClusterStateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClusterStateAsync(selector.InvokeOrDefault(new ClusterStateDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse, IClusterStateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.ClusterStateDispatchAsync<ClusterStateResponse>(p, c)
			);
	}
}
