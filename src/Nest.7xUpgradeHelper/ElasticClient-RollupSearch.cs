using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client,Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		RollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null)
			where THit : class
			where T : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		public static RollupSearchResponse<THit> RollupSearch<THit>(this IElasticClient client,IRollupSearchRequest request) where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client,Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken ct = default
		)
			where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		Task<RollupSearchResponse<THit>> RollupSearchAsync<T, THit>(Indices indices,
			Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null, CancellationToken ct = default
		)
			where THit : class
			where T : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		public static Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(this IElasticClient client,IRollupSearchRequest request, CancellationToken ct = default)
			where THit : class;
	}

}
