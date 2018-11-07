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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector) =>
			ReindexOnServer(selector.InvokeOrDefault(new ReindexOnServerDescriptor()));

		/// <inheritdoc />
		public IReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request) =>
			Dispatcher.Dispatch<IReindexOnServerRequest, ReindexOnServerRequestParameters, ReindexOnServerResponse>(
				ForceConfiguration<IReindexOnServerRequest, ReindexOnServerRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				LowLevelDispatch.ReindexDispatch<ReindexOnServerResponse>
			);

		/// <inheritdoc />
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ReindexOnServerAsync(selector.InvokeOrDefault(new ReindexOnServerDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IReindexOnServerRequest, ReindexOnServerRequestParameters, ReindexOnServerResponse, IReindexOnServerResponse>(
				ForceConfiguration<IReindexOnServerRequest, ReindexOnServerRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				cancellationToken,
				LowLevelDispatch.ReindexDispatchAsync<ReindexOnServerResponse>
			);
	}
}
