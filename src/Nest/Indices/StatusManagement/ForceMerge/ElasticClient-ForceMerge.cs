using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc/>
		IForceMergeResponse ForceMerge(IForceMergeRequest request);

		/// <inheritdoc/>
		Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc/>
		Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request);

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
		public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null) =>
			this.ForceMergeAsync(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request) =>
			this.Dispatcher.DispatchAsync<IForceMergeRequest, ForceMergeRequestParameters, ForceMergeResponse, IForceMergeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesForcemergeDispatchAsync<ForceMergeResponse>(p)
			);
	}
}
