using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAcknowledgedResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector);

		/// <inheritdoc/>
		IAcknowledgedResponse PutScript(IPutScriptRequest putScriptRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutScriptAsync(IPutScriptRequest putScriptRequest);

	}
	public partial class ElasticClient
	{
		public IAcknowledgedResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector) =>
			this.PutScript(putScriptSelector?.Invoke(new PutScriptDescriptor(language, id)));

		public IAcknowledgedResponse PutScript(IPutScriptRequest putScriptRequest) => 
			this.Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, AcknowledgedResponse>(
				putScriptRequest,
				this.LowLevelDispatch.PutScriptDispatch<AcknowledgedResponse>
			);

		public Task<IAcknowledgedResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector) => 
			this.PutScriptAsync(putScriptSelector?.Invoke(new PutScriptDescriptor(language, id)));

		public Task<IAcknowledgedResponse> PutScriptAsync(IPutScriptRequest putScriptRequest) => 
			this.Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				putScriptRequest,
				this.LowLevelDispatch.PutScriptDispatchAsync<AcknowledgedResponse>
			);
	}
}
