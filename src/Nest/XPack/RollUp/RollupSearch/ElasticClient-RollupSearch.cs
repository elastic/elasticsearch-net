using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class;
		IRollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null)
			where THit : class
			where T : class;

		/// <inheritdoc/>
		IRollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request) where THit : class;

		/// <inheritdoc/>
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(Indices indices,
			Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default)
			where THit : class;

		/// <inheritdoc/>
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<T, THit>(Indices indices,
			Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default)
			where THit : class
			where T : class;

		/// <inheritdoc/>
		Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken cancellationToken = default)
			where THit : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRollupSearchResponse<THit> RollupSearch<THit>(Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null)
			where THit : class =>
			this.RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)));

		public IRollupSearchResponse<THit> RollupSearch<T, THit>(Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null)
			where T : class
			where THit : class =>
			this.RollupSearch<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)));

		/// <inheritdoc/>
		public IRollupSearchResponse<THit> RollupSearch<THit>(IRollupSearchRequest request) where THit : class =>
			this.Dispatcher.Dispatch<IRollupSearchRequest, RollupSearchRequestParameters, RollupSearchResponse<THit>>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupRollupSearchDispatch<RollupSearchResponse<THit>>(p, d)
			);

		/// <inheritdoc/>
		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(
			Indices indices, Func<RollupSearchDescriptor<THit>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		) where THit : class =>
			this.RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<THit>(indices)), cancellationToken);

		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<T, THit>(
			Indices indices, Func<RollupSearchDescriptor<T>, IRollupSearchRequest> selector = null, CancellationToken cancellationToken = default
		)
			where T : class
			where THit : class =>
			this.RollupSearchAsync<THit>(selector.InvokeOrDefault(new RollupSearchDescriptor<T>(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IRollupSearchResponse<THit>> RollupSearchAsync<THit>(IRollupSearchRequest request, CancellationToken cancellationToken = default)
			where THit : class =>
			this.Dispatcher.DispatchAsync<IRollupSearchRequest, RollupSearchRequestParameters, RollupSearchResponse<THit>, IRollupSearchResponse<THit>>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupRollupSearchDispatchAsync<RollupSearchResponse<THit>>(p, d, c)
			);
	}
}
