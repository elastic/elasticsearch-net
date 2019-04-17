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
		RootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null);

		/// <inheritdoc />
		RootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request);

		/// <inheritdoc />
		Task<RootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<RootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null) =>
			RootNodeInfo(selector.InvokeOrDefault(new RootNodeInfoDescriptor()));

		/// <inheritdoc />
		public RootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request) =>
			DoRequest<IRootNodeInfoRequest, RootNodeInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<RootNodeInfoResponse> RootNodeInfoAsync(
			Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken ct = default
		) => RootNodeInfoAsync(selector.InvokeOrDefault(new RootNodeInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<RootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRootNodeInfoRequest, RootNodeInfoResponse>(request, request.RequestParameters, ct);
	}
}
