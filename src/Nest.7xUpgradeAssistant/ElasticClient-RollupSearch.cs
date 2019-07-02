using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.Search(), please update this usage.")]
		public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client, Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null
		)
			where THit : class
			=> client.Rollup.Search<THit>(s => selector?.InvokeOrDefault(s.Index(indices)));

		[Obsolete("Moved to client.Rollup.Search(), please update this usage.")]
		public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client, IRollupSearchRequest request)
			where THit : class
			=> client.Rollup.Search<THit>(request);

		[Obsolete("Moved to client.Rollup.SearchAsync(), please update this usage.")]
		public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client, Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken ct = default
		)
			where THit : class
			=> client.Rollup.SearchAsync<THit>(s => selector?.InvokeOrDefault(s.Index(indices)), ct);

		[Obsolete("Moved to client.Rollup.SearchAsync(), please update this usage.")]
		public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client, IRollupSearchRequest request,
			CancellationToken ct = default
		)
			where THit : class
			=> client.Rollup.SearchAsync<THit>(request, ct);
	}
}
