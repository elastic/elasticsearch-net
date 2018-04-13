using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.IO;
using System.Threading;

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
		Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IReindexOnServerResponse ReindexOnServer(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector) =>
			this.ReindexOnServer(selector.InvokeOrDefault(new ReindexOnServerDescriptor()));

		/// <inheritdoc/>
		public IReindexOnServerResponse ReindexOnServer(IReindexOnServerRequest request) =>
			this.Dispatcher.Dispatch<IReindexOnServerRequest, ReindexOnServerRequestParameters, ReindexOnServerResponse>(
				ForceConfiguration<IReindexOnServerRequest, ReindexOnServerRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				this.LowLevelDispatch.ReindexDispatch<ReindexOnServerResponse>
			);

		/// <inheritdoc/>
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ReindexOnServerAsync(selector.InvokeOrDefault(new ReindexOnServerDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IReindexOnServerRequest, ReindexOnServerRequestParameters, ReindexOnServerResponse, IReindexOnServerResponse>(
				ForceConfiguration<IReindexOnServerRequest, ReindexOnServerRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				cancellationToken,
				this.LowLevelDispatch.ReindexDispatchAsync<ReindexOnServerResponse>
			);
	}
}
