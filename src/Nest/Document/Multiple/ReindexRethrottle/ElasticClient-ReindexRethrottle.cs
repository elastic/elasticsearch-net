using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IReindexRethrottleResponse ReindexRethrottle(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector);

		/// <inheritdoc/>
		IReindexRethrottleResponse ReindexRethrottle(IReindexRethrottleRequest request);

		/// <inheritdoc/>
		Task<IReindexRethrottleResponse> ReindexRethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector);

		/// <inheritdoc/>
		Task<IReindexRethrottleResponse> ReindexRethrottleAsync(IReindexRethrottleRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IReindexRethrottleResponse ReindexRethrottle(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector) =>
			this.ReindexRethrottle(selector.InvokeOrDefault(new ReindexRethrottleDescriptor()));

		/// <inheritdoc/>
		public IReindexRethrottleResponse ReindexRethrottle(IReindexRethrottleRequest request) =>
			this.Dispatcher.Dispatch<IReindexRethrottleRequest, ReindexRethrottleRequestParameters, ReindexRethrottleResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ReindexRethrottleDispatch<ReindexRethrottleResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IReindexRethrottleResponse> ReindexRethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector) =>
			this.ReindexRethrottleAsync(selector.InvokeOrDefault(new ReindexRethrottleDescriptor()));

		/// <inheritdoc/>
		public Task<IReindexRethrottleResponse> ReindexRethrottleAsync(IReindexRethrottleRequest request) =>
			this.Dispatcher.DispatchAsync<IReindexRethrottleRequest, ReindexRethrottleRequestParameters, ReindexRethrottleResponse, IReindexRethrottleResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ReindexRethrottleDispatchAsync<ReindexRethrottleResponse>(p)
			);
	}
}
