using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Searches rolled-up data using the standard query DSL. The Rollup Search endpoint is needed because,
		/// internally, rolled-up documents utilize a different document structure than the original data.
		/// The Rollup Search endpoint rewrites standard query DSL into a format that matches the rollup documents,
		/// then takes the response and rewrites it back to what a client would expect given the original query.
		/// </summary>
		RollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		RollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null)
			where THit : class
			where T : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		RollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request) where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(Indices indices,
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
		Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken ct = default)
			where THit : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class =>
			RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)));

		/// <inheritdoc />
		public RollupSearchResponse<THit> RollupSearch<T, THit>(
			Indices indices,
			Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null
		)
			where T : class
			where THit : class =>
			RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)));

		/// <inheritdoc />
		public RollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request)
			where THit : class =>
			DoRequest<IRollupSearchRequest, RollupSearchResponse<THit>>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(
			Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null,
			CancellationToken ct = default
		)
			where THit : class =>
			RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)), ct);

		/// <inheritdoc />
		public Task<RollupSearchResponse<THit>> RollupSearchAsync<T, THit>(
			Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class
			where THit : class =>
			RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)), ct);

		/// <inheritdoc />
		public Task<RollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken ct = default)
			where THit : class =>
			DoRequestAsync<IRollupSearchRequest, RollupSearchResponse<THit>, RollupSearchResponse<THit>>
				(request, request.RequestParameters, ct);
	}
}
