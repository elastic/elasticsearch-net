using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetScriptResponse GetScript(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null);

		/// <inheritdoc />
		GetScriptResponse GetScript(IGetScriptRequest request);

		/// <inheritdoc />
		Task<GetScriptResponse> GetScriptAsync(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetScriptResponse GetScript(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null) =>
			GetScript(selector.InvokeOrDefault(new GetScriptDescriptor(id)));

		/// <inheritdoc />
		public GetScriptResponse GetScript(IGetScriptRequest request) =>
			DoRequest<IGetScriptRequest, GetScriptResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetScriptResponse> GetScriptAsync(
			Id id,
			Func<GetScriptDescriptor, IGetScriptRequest> selector = null,
			CancellationToken ct = default
		) => GetScriptAsync(selector.InvokeOrDefault(new GetScriptDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<GetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetScriptRequest, GetScriptResponse>(request, request.RequestParameters, ct);
	}
}
