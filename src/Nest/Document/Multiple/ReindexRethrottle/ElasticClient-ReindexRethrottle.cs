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
		ReindexRethrottleResponse Rethrottle(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		ReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<ReindexRethrottleResponse> RethrottleAsync(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		Task<ReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public ReindexRethrottleResponse Rethrottle(TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null) =>
			Rethrottle(selector.InvokeOrDefault(new ReindexRethrottleDescriptor(id)));

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public ReindexRethrottleResponse Rethrottle(IReindexRethrottleRequest request) =>
			DoRequest<IReindexRethrottleRequest, ReindexRethrottleResponse>(request, request.RequestParameters);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<ReindexRethrottleResponse> RethrottleAsync(TaskId id,
			Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null,
			CancellationToken ct = default
		) =>
			RethrottleAsync(selector.InvokeOrDefault(new ReindexRethrottleDescriptor(id)), ct);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public Task<ReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IReindexRethrottleRequest, ReindexRethrottleResponse>(request, request.RequestParameters, ct);
	}
}
