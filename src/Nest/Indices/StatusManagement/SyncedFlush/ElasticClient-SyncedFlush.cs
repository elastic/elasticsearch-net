using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The Synced Flush API allows an administrator to initiate a synced flush manually.
		/// This can be particularly useful for a planned (rolling) cluster restart where you
		/// can stop indexing and don’t want to wait the default 5 minutes for idle indices to
		/// be sync-flushed automatically.
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the synced flush operation</param>
		/// <returns></returns>
		SyncedFlushResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null);

		/// <inheritdoc />
		SyncedFlushResponse SyncedFlush(ISyncedFlushRequest request);

		/// <inheritdoc />
		Task<SyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<SyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SyncedFlushResponse SyncedFlush(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null) =>
			SyncedFlush(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)));

		/// <inheritdoc />
		public SyncedFlushResponse SyncedFlush(ISyncedFlushRequest request) =>
			DoRequest<ISyncedFlushRequest, SyncedFlushResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SyncedFlushResponse> SyncedFlushAsync(
			Indices indices,
			Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector = null,
			CancellationToken cancellationToken = default
		) => SyncedFlushAsync(selector.InvokeOrDefault(new SyncedFlushDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<SyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISyncedFlushRequest, SyncedFlushResponse>(request, request.RequestParameters, ct);
	}
}
