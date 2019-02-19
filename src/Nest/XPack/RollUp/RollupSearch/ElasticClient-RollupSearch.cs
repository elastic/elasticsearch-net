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
		IRollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		IRollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null)
			where THit : class
			where T : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		IRollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request) where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		)
			where THit : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<T, THit>(Indices indices,
			Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		)
			where THit : class
			where T : class;

		/// <inheritdoc cref="RollupSearch{THit}(Nest.Indices,System.Func{Nest.RollupSearchDescriptor{THit},Nest.IRollupSearchRequest})" />
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken cancellationToken = default)
			where THit : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class =>
			RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)));

		/// <inheritdoc />
		public IRollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null
		)
			where T : class
			where THit : class =>
			RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)));

		/// <inheritdoc />
		public IRollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request) where THit : class =>
			Dispatcher.Dispatch<IRollupSearchRequest, RollupSearchRequestParameters, RollupSearchResponse<THit>>(
				request,
				(p, d) => LowLevelDispatch.RollupRollupSearchDispatch<RollupSearchResponse<THit>>(p, d)
			);

		/// <inheritdoc />
		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(
			Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		) where THit : class =>
			RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<T, THit>(
			Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		)
			where T : class
			where THit : class =>
			RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken cancellationToken = default)
			where THit : class =>
			Dispatcher.DispatchAsync<IRollupSearchRequest, RollupSearchRequestParameters, RollupSearchResponse<THit>, IRollupSearchResponse<THit>>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.RollupRollupSearchDispatchAsync<RollupSearchResponse<THit>>(p, d, c)
			);
	}
}
