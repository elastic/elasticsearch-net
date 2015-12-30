using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutScriptResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc/>
		IPutScriptResponse PutScript(IPutScriptRequest request);

		/// <inheritdoc/>
		Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc/>
		Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request);

	}
	public partial class ElasticClient
	{
		public IPutScriptResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) =>
			this.PutScript(selector?.Invoke(new PutScriptDescriptor(language, id)));

		public IPutScriptResponse PutScript(IPutScriptRequest request) => 
			this.Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse>(
				request,
				this.LowLevelDispatch.PutScriptDispatch<PutScriptResponse>
			);

		public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) => 
			this.PutScriptAsync(selector?.Invoke(new PutScriptDescriptor(language, id)));

		public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request) => 
			this.Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse, IPutScriptResponse>(
				request,
				this.LowLevelDispatch.PutScriptDispatchAsync<PutScriptResponse>
			);
	}
}
