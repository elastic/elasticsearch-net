using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{

		/// <inheritdoc/>
		IGetScriptResponse GetScript(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null);

		/// <inheritdoc/>
		IGetScriptResponse GetScript(IGetScriptRequest request);

		/// <inheritdoc/>
		Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetScriptResponse GetScript(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null) =>
			this.GetScript(selector.InvokeOrDefault(new GetScriptDescriptor(language, id)));

		/// <inheritdoc/>
		public IGetScriptResponse GetScript(IGetScriptRequest request) =>
			this.Dispatcher.Dispatch<IGetScriptRequest, GetScriptRequestParameters, GetScriptResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetScriptDispatch<GetScriptResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetScriptAsync(selector.InvokeOrDefault(new GetScriptDescriptor(language, id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetScriptRequest, GetScriptRequestParameters, GetScriptResponse, IGetScriptResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.GetScriptDispatchAsync<GetScriptResponse>(p, c)
			);
	}
}
