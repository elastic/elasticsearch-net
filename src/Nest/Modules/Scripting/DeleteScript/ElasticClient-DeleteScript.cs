using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(IDeleteScriptRequest request);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request);

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
		public IDeleteScriptResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			this.DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) => 
			this.DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
			);
	}
}
