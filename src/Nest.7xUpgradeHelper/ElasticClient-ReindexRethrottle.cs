using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public static ReindexRethrottleResponse Rethrottle(this IElasticClient client,TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public static ReindexRethrottleResponse Rethrottle(this IElasticClient client,IReindexRethrottleRequest request);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public static Task<ReindexRethrottleResponse> RethrottleAsync(this IElasticClient client,TaskId id, Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Rethrottle an existing reindex or update by query task
		/// </summary>
		public static Task<ReindexRethrottleResponse> RethrottleAsync(this IElasticClient client,IReindexRethrottleRequest request,
			CancellationToken ct = default
		);
	}

}
