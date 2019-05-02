using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		PutScriptResponse PutScript(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc />
		PutScriptResponse PutScript(IPutScriptRequest request);

		/// <inheritdoc />
		Task<PutScriptResponse> PutScriptAsync(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<PutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public PutScriptResponse PutScript(Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector) =>
			PutScript(selector?.Invoke(new PutScriptDescriptor(id)));

		public PutScriptResponse PutScript(IPutScriptRequest request) =>
			DoRequest<IPutScriptRequest, PutScriptResponse>(request, request.RequestParameters);

		public Task<PutScriptResponse> PutScriptAsync(
			Id id,
			Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken ct = default
		) => PutScriptAsync(selector?.Invoke(new PutScriptDescriptor(id)), ct);

		public Task<PutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutScriptRequest, PutScriptResponse>(request, request.RequestParameters, ct);
	}
}
