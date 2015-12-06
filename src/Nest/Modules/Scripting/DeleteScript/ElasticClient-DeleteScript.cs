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
		IAcknowledgedResponse DeleteScript(IDeleteScriptRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteScriptAsync(IDeleteScriptRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteScript(IDeleteScriptRequest request) => 
			this.Dispatcher.Dispatch<IDeleteScriptRequest, DeleteScriptRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteScript(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			this.DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) => 
			this.DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteScriptAsync(IDeleteScriptRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteScriptRequest, DeleteScriptRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}
