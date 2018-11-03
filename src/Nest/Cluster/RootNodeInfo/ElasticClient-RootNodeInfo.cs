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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null) =>
			RootNodeInfo(selector.InvokeOrDefault(new RootNodeInfoDescriptor()));

		/// <inheritdoc />
		public IRootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request) =>
			Dispatcher.Dispatch<IRootNodeInfoRequest, RootNodeInfoRequestParameters, RootNodeInfoResponse>(
				request,
				(p, d) => LowLevelDispatch.InfoDispatch<RootNodeInfoResponse>(p)
			);

		/// <inheritdoc />
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RootNodeInfoAsync(selector.InvokeOrDefault(new RootNodeInfoDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRootNodeInfoRequest, RootNodeInfoRequestParameters, RootNodeInfoResponse, IRootNodeInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.InfoDispatchAsync<RootNodeInfoResponse>(p, c)
			);
	}
}
