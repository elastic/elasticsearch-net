using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc />
		IForceMergeResponse ForceMerge(IForceMergeRequest request);

		/// <inheritdoc />
		Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null) =>
			ForceMerge(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)));

		/// <inheritdoc />
		public IForceMergeResponse ForceMerge(IForceMergeRequest request) =>
			Dispatcher.Dispatch<IForceMergeRequest, ForceMergeRequestParameters, ForceMergeResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesForcemergeDispatch<ForceMergeResponse>(p)
			);

		/// <inheritdoc />
		public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ForceMergeAsync(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IForceMergeRequest, ForceMergeRequestParameters, ForceMergeResponse, IForceMergeResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesForcemergeDispatchAsync<ForceMergeResponse>(p, c)
			);
	}
}
