using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Executes an arbitrary Painless script and returns a result.
		/// Useful for testing the syntactical correctness of Painless scripts
		/// </summary>
		IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector
		);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(IExecutePainlessScriptRequest request);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(IExecutePainlessScriptRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector
		) =>
			ExecutePainlessScript<TResult>(selector?.Invoke(new ExecutePainlessScriptDescriptor()));

		/// <inheritdoc />
		public IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(IExecutePainlessScriptRequest request) =>
			DoRequest<IExecutePainlessScriptRequest, ExecutePainlessScriptResponse<TResult>>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken ct = default
		) => ExecutePainlessScriptAsync<TResult>(selector?.Invoke(new ExecutePainlessScriptDescriptor()), ct);

		/// <inheritdoc />
		public Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(
			IExecutePainlessScriptRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IExecutePainlessScriptRequest, IExecutePainlessScriptResponse<TResult>, ExecutePainlessScriptResponse<TResult>>
				(request, request.RequestParameters, ct);
	}
}
