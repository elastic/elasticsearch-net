using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		IRootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null);

		/// <inheritdoc />
		IRootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request);

		/// <inheritdoc />
		Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null) =>
			RootNodeInfo(selector.InvokeOrDefault(new RootNodeInfoDescriptor()));

		/// <inheritdoc />
		public IRootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request) =>
			Dispatch2<IRootNodeInfoRequest, RootNodeInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(
			Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken ct = default
		) => RootNodeInfoAsync(selector.InvokeOrDefault(new RootNodeInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IRootNodeInfoRequest, IRootNodeInfoResponse, RootNodeInfoResponse>(request, request.RequestParameters, ct);
	}
}
