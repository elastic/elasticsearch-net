using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Executes an arbitrary Painless script and returns a result.
		/// Useful for testing the syntactical correctness of Painless scripts
		/// </summary>
		IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})"/>
		IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(IExecutePainlessScriptRequest request);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})"/>
		Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken cancellationToken = default);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})"/>
		Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector) =>
			this.ExecutePainlessScript<TResult>(selector?.Invoke(new ExecutePainlessScriptDescriptor()));

		/// <inheritdoc />
		public IExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(IExecutePainlessScriptRequest request) =>
			this.Dispatcher.Dispatch<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse<TResult>>(
				request,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatch<ExecutePainlessScriptResponse<TResult>>
			);

		/// <inheritdoc />
		public Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken cancellationToken = default) =>
			this.ExecutePainlessScriptAsync<TResult>(selector?.Invoke(new ExecutePainlessScriptDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse<TResult>, IExecutePainlessScriptResponse<TResult>>(
				request,
				cancellationToken,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatchAsync<ExecutePainlessScriptResponse<TResult>>
			);
	}
}
