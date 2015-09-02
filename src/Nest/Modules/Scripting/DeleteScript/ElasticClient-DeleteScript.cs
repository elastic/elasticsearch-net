using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector);

		/// <inheritdoc/>
		IDeleteScriptResponse DeleteScript(IDeleteScriptRequest deleteScriptRequest);

		/// <inheritdoc/>
		Task<IDeleteScriptResponse> DeleteScriptAsync(Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector);

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
		public IDeleteScriptResponse DeleteScript(Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector) => 
			this.Dispatcher.Dispatch<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse>(
				deleteScriptSelector?.Invoke(new DeleteScriptDescriptor()),
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatch<DeleteScriptResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(Func<DeleteScriptDescriptor, IDeleteScriptRequest> deleteScriptSelector) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
				deleteScriptSelector?.Invoke(new DeleteScriptDescriptor()),
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest deleteScriptRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
				deleteScriptRequest,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
			);
	}
}
