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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request);

		/// <inheritdoc />
		Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector) =>
			ClusterReroute(selector?.Invoke(new ClusterRerouteDescriptor()));

		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest request) =>
			DoRequest<IClusterRerouteRequest, ClusterRerouteResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		) =>
			ClusterRerouteAsync(selector?.Invoke(new ClusterRerouteDescriptor()), ct);

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterRerouteRequest, IClusterRerouteResponse, ClusterRerouteResponse>(request, request.RequestParameters, ct);
	}
}
