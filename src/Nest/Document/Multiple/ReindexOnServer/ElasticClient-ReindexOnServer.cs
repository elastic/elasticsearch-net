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
		IReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		IReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken ct = default
		);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		private static readonly int[] ReindexOnServerAllowedStatusCodes = { -1 };

		/// <inheritdoc />
		public IReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector) =>
			ReindexOnServer(selector.InvokeOrDefault(new ReindexOnServerDescriptor()));

		/// <inheritdoc />
		public IReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request)
		{
			ForceConfiguration(request, c => c.AllowedStatusCodes = ReindexOnServerAllowedStatusCodes);
			return Dispatch2<IReindexOnServerRequest, ReindexOnServerResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(
			Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken ct = default
		) => ReindexOnServerAsync(selector.InvokeOrDefault(new ReindexOnServerDescriptor()), ct);

		/// <inheritdoc />
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request, CancellationToken ct = default)
		{
			ForceConfiguration(request, c => c.AllowedStatusCodes = ReindexOnServerAllowedStatusCodes);
			return Dispatch2Async<IReindexOnServerRequest, IReindexOnServerResponse, ReindexOnServerResponse>(request, request.RequestParameters, ct);
		}
	}
}
