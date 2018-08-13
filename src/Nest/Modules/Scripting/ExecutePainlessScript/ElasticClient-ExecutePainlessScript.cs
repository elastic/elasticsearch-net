using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExecutePainlessScriptResponse ExecutePainlessScript(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector);

		/// <inheritdoc/>
		IExecutePainlessScriptResponse ExecutePainlessScript(IExecutePainlessScriptRequest request);

		/// <inheritdoc/>
		Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		public IExecutePainlessScriptResponse ExecutePainlessScript(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector) =>
			this.ExecutePainlessScript(selector?.Invoke(new ExecutePainlessScriptDescriptor()));

		public IExecutePainlessScriptResponse ExecutePainlessScript(IExecutePainlessScriptRequest request) =>
			this.Dispatcher.Dispatch<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse>(
				request,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatch<ExecutePainlessScriptResponse>
			);

		public Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ExecutePainlessScriptAsync(selector?.Invoke(new ExecutePainlessScriptDescriptor()), cancellationToken);

		public Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse, IExecutePainlessScriptResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatchAsync<ExecutePainlessScriptResponse>
			);
	}
}
