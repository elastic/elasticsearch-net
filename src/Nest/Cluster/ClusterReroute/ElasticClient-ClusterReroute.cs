using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands.
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled,
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector);

		/// <inheritdoc />
		Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request);

		/// <inheritdoc />
		Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector) =>
			ClusterReroute(selector?.Invoke(new ClusterRerouteDescriptor()));

		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request) =>
			Dispatcher.Dispatch<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse>(
				request,
				LowLevelDispatch.ClusterRerouteDispatch<ClusterRerouteResponse>
			);

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClusterRerouteAsync(selector?.Invoke(new ClusterRerouteDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse, IClusterRerouteResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.ClusterRerouteDispatchAsync<ClusterRerouteResponse>
			);
	}
}
