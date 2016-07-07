using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc/>
		IForceMergeResponse ForceMerge(IForceMergeRequest request);

		/// <inheritdoc/>
		Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null) =>
			this.ForceMerge(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IForceMergeResponse ForceMerge(IForceMergeRequest request) =>
			this.Dispatcher.Dispatch<IForceMergeRequest, ForceMergeRequestParameters, ForceMergeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesForcemergeDispatch<ForceMergeResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ForceMergeAsync(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IForceMergeRequest, ForceMergeRequestParameters, ForceMergeResponse, IForceMergeResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesForcemergeDispatchAsync<ForceMergeResponse>(p, c)
			);
	}
}
