using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector = null);

		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(IDeleteScriptRequest deleteScriptRequest);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector = null);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest deleteScriptRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteScriptResponse DeleteScript(IDeleteScriptRequest deleteScriptRequest) => 
			this.Dispatcher.Dispatch<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse>(
				deleteScriptRequest,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatch<DeleteScriptResponse>(p)
			);

		/// <inheritdoc/>
		public IDeleteScriptResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector = null) =>
			this.DeleteScript(deleteScriptSelector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector = null) => 
			this.DeleteScriptAsync(deleteScriptSelector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest deleteScriptRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
				deleteScriptRequest,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
			);
	}
}
