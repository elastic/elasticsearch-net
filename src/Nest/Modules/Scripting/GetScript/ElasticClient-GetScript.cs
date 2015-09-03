using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{

		/// <inheritdoc/>
		IGetScriptResponse GetScript(Func<GetScriptDescriptor, IGetScriptRequest> getScriptSelector);

		/// <inheritdoc/>
		IGetScriptResponse GetScript(IGetScriptRequest getScriptRequest);

		/// <inheritdoc/>
		Task<IGetScriptResponse> GetScriptAsync(Func<GetScriptDescriptor, IGetScriptRequest> getScriptSelector);

		/// <inheritdoc/>
		Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest getScriptRequest);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetScriptResponse GetScript(Func<GetScriptDescriptor, IGetScriptRequest> getScriptSelector) =>
			this.GetScript(getScriptSelector?.Invoke(new GetScriptDescriptor()));

		/// <inheritdoc/>
		public IGetScriptResponse GetScript(IGetScriptRequest getScriptRequest) => 
			this.Dispatcher.Dispatch<IGetScriptRequest, GetScriptRequestParameters, GetScriptResponse>(
				getScriptRequest,
				(p, d) => this.LowLevelDispatch.GetScriptDispatch<GetScriptResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetScriptResponse> GetScriptAsync(Func<GetScriptDescriptor, IGetScriptRequest> getScriptSelector) => 
			this.GetScriptAsync(getScriptSelector?.Invoke(new GetScriptDescriptor()));

		/// <inheritdoc/>
		public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest getScriptRequest) => 
			this.Dispatcher.DispatchAsync<IGetScriptRequest, GetScriptRequestParameters, GetScriptResponse, IGetScriptResponse>(
				getScriptRequest,
				(p, d) => this.LowLevelDispatch.GetScriptDispatchAsync<GetScriptResponse>(p)
			);
	}
}
