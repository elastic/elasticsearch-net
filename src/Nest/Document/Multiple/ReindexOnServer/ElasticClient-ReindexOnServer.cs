using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		ReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		ReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<ReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken ct = default
		);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<ReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc />
		public ReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector) =>
			ReindexOnServer(selector.InvokeOrDefault(new ReindexOnServerDescriptor()));

		/// <inheritdoc />
		public ReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request) =>
			DoRequest<IReindexOnServerRequest, ReindexOnServerResponse>(request, request.RequestParameters, r => AcceptAllStatusCodesHandler(r));

		/// <inheritdoc />
		public Task<ReindexOnServerResponse> ReindexOnServerAsync(
			Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken ct = default
		) => ReindexOnServerAsync(selector.InvokeOrDefault(new ReindexOnServerDescriptor()), ct);

		/// <inheritdoc />
		public Task<ReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IReindexOnServerRequest, ReindexOnServerResponse>
				(request, request.RequestParameters, ct, r => AcceptAllStatusCodesHandler(r));
	}
}
