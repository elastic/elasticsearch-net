using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExecutePainlessScriptResponse ExecutePainlessScript(string source, Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector = null);

		/// <inheritdoc/>
		IExecutePainlessScriptResponse ExecutePainlessScript(IExecutePainlessScriptRequest request);

		/// <inheritdoc/>
		Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(string source, Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		public IExecutePainlessScriptResponse ExecutePainlessScript(string source, Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector = null) =>
			this.ExecutePainlessScript(selector?.Invoke(new ExecutePainlessScriptDescriptor().Painless(source)));

		public IExecutePainlessScriptResponse ExecutePainlessScript(IExecutePainlessScriptRequest request) =>
			this.Dispatcher.Dispatch<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse>(
				request,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatch<ExecutePainlessScriptResponse>
			);

		public Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(string source, Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ExecutePainlessScriptAsync(selector?.Invoke(new ExecutePainlessScriptDescriptor().Painless(source)), cancellationToken);

		public Task<IExecutePainlessScriptResponse> ExecutePainlessScriptAsync(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IExecutePainlessScriptRequest, ExecutePainlessScriptRequestParameters, ExecutePainlessScriptResponse, IExecutePainlessScriptResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.ScriptsPainlessExecuteDispatchAsync<ExecutePainlessScriptResponse>
			);
	}
}
