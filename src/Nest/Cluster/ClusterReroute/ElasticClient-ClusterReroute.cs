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
		ClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector);

		/// <inheritdoc />
		Task<ClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		ClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request);

		/// <inheritdoc />
		Task<ClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector) =>
			ClusterReroute(selector?.Invoke(new ClusterRerouteDescriptor()));

		/// <inheritdoc />
		public ClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request) =>
			DoRequest<IClusterRerouteRequest, ClusterRerouteResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		) =>
			ClusterRerouteAsync(selector?.Invoke(new ClusterRerouteDescriptor()), ct);

		/// <inheritdoc />
		public Task<ClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterRerouteRequest, ClusterRerouteResponse, ClusterRerouteResponse>(request, request.RequestParameters, ct);
	}
}
