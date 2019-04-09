using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		IReindexRethrottleResponse Rethrottle(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		IReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<IReindexRethrottleResponse> RethrottleAsync(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public IReindexRethrottleResponse Rethrottle(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null) =>
			Rethrottle(selector.InvokeOrDefault(new ReindexRethrottleDescriptor(id)));

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public IReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request) =>
			Dispatch2<IReindexRethrottleRequest, ReindexRethrottleResponse>(request, request.RequestParameters);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<IReindexRethrottleResponse> RethrottleAsync(TaskId id,
			Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null,
			CancellationToken ct = default
		) =>
			RethrottleAsync(selector.InvokeOrDefault(new ReindexRethrottleDescriptor(id)), ct);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IReindexRethrottleRequest, IReindexRethrottleResponse, ReindexRethrottleResponse>(request, request.RequestParameters, ct);
	}
}
