using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutScriptResponse PutScript(Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector);

		/// <inheritdoc/>
		IPutScriptResponse PutScript(IPutScriptRequest putScriptRequest);

		/// <inheritdoc/>
		Task<IPutScriptResponse> PutScriptAsync(Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector);

		/// <inheritdoc/>
		Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest putScriptRequest);

	}
	public partial class ElasticClient
	{
		public IPutScriptResponse PutScript(Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector) => 
			this.Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse>(
				putScriptSelector?.Invoke(new PutScriptDescriptor()),
				this.LowLevelDispatch.PutScriptDispatch<PutScriptResponse>
			);

		public IPutScriptResponse PutScript(IPutScriptRequest putScriptRequest) => 
			this.Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse>(
				putScriptRequest,
				this.LowLevelDispatch.PutScriptDispatch<PutScriptResponse>
			);

		public Task<IPutScriptResponse> PutScriptAsync(Func<PutScriptDescriptor, IPutScriptRequest> putScriptSelector) => 
			this.Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse, IPutScriptResponse>(
				putScriptSelector?.Invoke(new PutScriptDescriptor()),
				this.LowLevelDispatch.PutScriptDispatchAsync<PutScriptResponse>
			);

		public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest putScriptRequest) => 
			this.Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse, IPutScriptResponse>(
				putScriptRequest,
				this.LowLevelDispatch.PutScriptDispatchAsync<PutScriptResponse>
			);
	}
}
