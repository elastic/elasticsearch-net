using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		IReindexRethrottleResponse Rethrottle(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		IReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public IReindexRethrottleResponse Rethrottle(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector) =>
			this.Rethrottle(selector.InvokeOrDefault(new ReindexRethrottleDescriptor()));

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public IReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request) =>
			this.Dispatcher.Dispatch<IReindexRethrottleRequest, ReindexRethrottleRequestParameters, ReindexRethrottleResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ReindexRethrottleDispatch<ReindexRethrottleResponse>(p)
			);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.RethrottleAsync(selector.InvokeOrDefault(new ReindexRethrottleDescriptor()), cancellationToken);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IReindexRethrottleRequest, ReindexRethrottleRequestParameters, ReindexRethrottleResponse, IReindexRethrottleResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.ReindexRethrottleDispatchAsync<ReindexRethrottleResponse>(p, c)
			);
	}
}
