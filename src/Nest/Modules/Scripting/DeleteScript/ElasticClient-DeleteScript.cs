using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc />
		IDeleteScriptResponse DeleteScript(IDeleteScriptRequest request);

		/// <inheritdoc />
		Task<IDeleteScriptResponse> DeleteScriptAsync(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteScriptResponse DeleteScript(IDeleteScriptRequest request) =>
			DoRequest<IDeleteScriptRequest, DeleteScriptResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public IDeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)));

		/// <inheritdoc />
		public Task<IDeleteScriptResponse> DeleteScriptAsync(
			Id id,
			Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null,
			CancellationToken ct = default
		) => DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteScriptRequest, IDeleteScriptResponse, DeleteScriptResponse>(request, request.RequestParameters, ct);
	}
}
