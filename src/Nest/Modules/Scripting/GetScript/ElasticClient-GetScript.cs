using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetScriptResponse GetScript(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null);

		/// <inheritdoc />
		IGetScriptResponse GetScript(IGetScriptRequest request);

		/// <inheritdoc />
		Task<IGetScriptResponse> GetScriptAsync(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetScriptResponse GetScript(Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null) =>
			GetScript(selector.InvokeOrDefault(new GetScriptDescriptor(id)));

		/// <inheritdoc />
		public IGetScriptResponse GetScript(IGetScriptRequest request) =>
			DoRequest<IGetScriptRequest, GetScriptResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetScriptResponse> GetScriptAsync(
			Id id,
			Func<GetScriptDescriptor, IGetScriptRequest> selector = null,
			CancellationToken ct = default
		) => GetScriptAsync(selector.InvokeOrDefault(new GetScriptDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetScriptRequest, IGetScriptResponse, GetScriptResponse>(request, request.RequestParameters, ct);
	}
}
