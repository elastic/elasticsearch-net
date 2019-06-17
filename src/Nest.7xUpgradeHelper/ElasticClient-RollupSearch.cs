using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Searches rolled-up data using the standard query DSL. The Rollup Search endpoint is needed because,
		/// internally, rolled-up documents utilize a different document structure than the original data.
		/// The Rollup Search endpoint rewrites standard query DSL into a format that matches the rollup documents,
		/// then takes the response and rewrites it back to what a client would expect given the original query.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client, Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null
		)
			where THit : class
			=> client.Rollup.Search<THit>(s => selector?.InvokeOrDefault(s.Index(indices)));

		/// <inheritdoc
		///     cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client, IRollupSearchRequest request) 
			where THit : class
			=> client.Rollup.Search<THit>(request);

		/// <inheritdoc
		///     cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client, Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken ct = default
		)
			where THit : class
			=> client.Rollup.SearchAsync<THit>(s => selector?.InvokeOrDefault(s.Index(indices)), ct);

		/// <inheritdoc
		///     cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client, IRollupSearchRequest request,
			CancellationToken ct = default
		)
			where THit : class
			=> client.Rollup.SearchAsync<THit>(request, ct);
	}
}
