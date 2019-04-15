using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc />
		DeleteScriptResponse DeleteScript(IDeleteScriptRequest request);

		/// <inheritdoc />
		Task<DeleteScriptResponse> DeleteScriptAsync(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteScriptResponse DeleteScript(IDeleteScriptRequest request) =>
			DoRequest<IDeleteScriptRequest, DeleteScriptResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public DeleteScriptResponse DeleteScript(Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null) =>
			DeleteScript(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)));

		/// <inheritdoc />
		public Task<DeleteScriptResponse> DeleteScriptAsync(
			Id id,
			Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null,
			CancellationToken ct = default
		) => DeleteScriptAsync(selector.InvokeOrDefault(new DeleteScriptDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<DeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteScriptRequest, DeleteScriptResponse, DeleteScriptResponse>(request, request.RequestParameters, ct);
	}
}
