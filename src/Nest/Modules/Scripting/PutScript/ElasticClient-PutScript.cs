using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAcknowledgedResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse PutScript(IPutScriptRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutScriptAsync(IPutScriptRequest request);

	}
	public partial class ElasticClient
	{
		public IAcknowledgedResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) =>
			this.PutScript(selector?.Invoke(new PutScriptDescriptor(language, id)));

		public IAcknowledgedResponse PutScript(IPutScriptRequest request) => 
			this.Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.PutScriptDispatch<AcknowledgedResponse>
			);

		public Task<IAcknowledgedResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) => 
			this.PutScriptAsync(selector?.Invoke(new PutScriptDescriptor(language, id)));

		public Task<IAcknowledgedResponse> PutScriptAsync(IPutScriptRequest request) => 
			this.Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.PutScriptDispatchAsync<AcknowledgedResponse>
			);
	}
}
