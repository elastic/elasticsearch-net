using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutScriptResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc />
		IPutScriptResponse PutScript(IPutScriptRequest request);

		/// <inheritdoc />
		Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IPutScriptResponse PutScript(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) =>
			PutScript(selector?.Invoke(new PutScriptDescriptor(language, id)));

		public IPutScriptResponse PutScript(IPutScriptRequest request) =>
			Dispatcher.Dispatch<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse>(
				request,
				LowLevelDispatch.PutScriptDispatch<PutScriptResponse>
			);

		public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutScriptAsync(selector?.Invoke(new PutScriptDescriptor(language, id)), cancellationToken);

		public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IPutScriptRequest, PutScriptRequestParameters, PutScriptResponse, IPutScriptResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.PutScriptDispatchAsync<PutScriptResponse>
			);
	}
}
