using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutScriptResponse PutScript(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc />
		IPutScriptResponse PutScript(IPutScriptRequest request);

		/// <inheritdoc />
		Task<IPutScriptResponse> PutScriptAsync(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public IPutScriptResponse PutScript(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) =>
			PutScript(selector?.Invoke(new PutScriptDescriptor(id)));

		public IPutScriptResponse PutScript(IPutScriptRequest request) =>
			Dispatch2<IPutScriptRequest, PutScriptResponse>(request, request.RequestParameters);

		public Task<IPutScriptResponse> PutScriptAsync(
			Id id,
			Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken ct = default
		) => PutScriptAsync(selector?.Invoke(new PutScriptDescriptor(id)), ct);

		public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutScriptRequest, IPutScriptResponse, PutScriptResponse>(request, request.RequestParameters, ct);
	}
}
