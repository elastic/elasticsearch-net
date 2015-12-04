using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAcknowledgedResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteScript(IDeleteScriptRequest deleteScriptRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteScriptAsync(IDeleteScriptRequest deleteScriptRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteScript(IDeleteScriptRequest deleteScriptRequest) => 
			this.Dispatcher.Dispatch<IDeleteScriptRequest, DeleteScriptRequestParameters, AcknowledgedResponse>(
				deleteScriptRequest,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			this.DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) => 
			this.DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteScriptAsync(IDeleteScriptRequest deleteScriptRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				deleteScriptRequest,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}
