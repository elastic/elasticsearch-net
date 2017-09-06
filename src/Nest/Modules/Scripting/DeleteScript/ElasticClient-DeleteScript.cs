using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(IDeleteScriptRequest request);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteScriptResponse DeleteScript(IDeleteScriptRequest request) =>
			this.Dispatcher.Dispatch<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatch<DeleteScriptResponse>(p)
			);

		/// <inheritdoc/>
		public IDeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			this.DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)));

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p, c)
			);
	}
}
