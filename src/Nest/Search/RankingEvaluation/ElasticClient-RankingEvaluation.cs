using System;
using System.Threading.Tasks;
using System.Threading;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The ranking evaluation API allows to evaluate the quality of ranked search results
		/// over a set of typical search queries. Given this set of queries and a list of
		/// manually rated documents, the _rank_eval endpoint calculates and returns typical
		/// information retrieval metrics like mean reciprocal rank, precision or
		/// discounted cumulative gain.
		/// </summary>
		IRankingEvaluationResponse<T> RankingEvaluation<T>(IRankingEvaluationRequest request) where T : class;

		///<inheritdoc cref="RankingEvaluation{T}(Nest.IRankingEvaluationRequest)"/>
		IRankingEvaluationResponse<T> RankingEvaluation<T>(Func<RankingEvaluationDescriptor<T>, IRankingEvaluationRequest> selector = null)
			where T : class;

		///<inheritdoc cref="RankingEvaluation{T}(Nest.IRankingEvaluationRequest)"/>
		Task<IRankingEvaluationResponse<T>> RankingEvaluationAsync<T>(IRankingEvaluationRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		///<inheritdoc cref="RankingEvaluation{T}(Nest.IRankingEvaluationRequest)"/>
		Task<IRankingEvaluationResponse<T>> RankingEvaluationAsync<T>(Func<RankingEvaluationDescriptor<T>, IRankingEvaluationRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRankingEvaluationResponse<T> RankingEvaluation<T>(IRankingEvaluationRequest request) where T : class =>
			this.Dispatcher.Dispatch<IRankingEvaluationRequest, RankingEvaluationRequestParameters, RankingEvaluationResponse<T>>(
				request,
				this.LowLevelDispatch.RankEvalDispatch<RankingEvaluationResponse<T>>
			);

		/// <inheritdoc/>
		public IRankingEvaluationResponse<T> RankingEvaluation<T>(Func<RankingEvaluationDescriptor<T>, IRankingEvaluationRequest> selector = null) where T : class =>
			this.RankingEvaluation<T>(selector.InvokeOrDefault(new RankingEvaluationDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IRankingEvaluationResponse<T>> RankingEvaluationAsync<T>(IRankingEvaluationRequest request, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.Dispatcher.DispatchAsync<IRankingEvaluationRequest, RankingEvaluationRequestParameters, RankingEvaluationResponse<T>, IRankingEvaluationResponse<T>>(
				request,
				cancellationToken,
				this.LowLevelDispatch.RankEvalDispatchAsync<RankingEvaluationResponse<T>>
			);

		/// <inheritdoc/>
		public Task<IRankingEvaluationResponse<T>> RankingEvaluationAsync<T>(Func<RankingEvaluationDescriptor<T>, IRankingEvaluationRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.RankingEvaluationAsync<T>(selector.InvokeOrDefault(new RankingEvaluationDescriptor<T>()), cancellationToken);
	}
}
