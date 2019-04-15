using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The flush API allows to flush one or more indices through an API. The flush process of an index basically
		/// frees memory from the index by flushing data to the index storage and clearing the internal transaction log.
		/// By default, Elasticsearch uses memory heuristics in order to automatically trigger
		/// flush operations as required in order to clear memory.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-flush.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the flush operation</param>
		FlushResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null);

		/// <inheritdoc />
		FlushResponse Flush(IFlushRequest request);

		/// <inheritdoc />
		Task<FlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<FlushResponse> FlushAsync(IFlushRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public FlushResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null) =>
			Flush(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)));

		/// <inheritdoc />
		public FlushResponse Flush(IFlushRequest request) =>
			DoRequest<IFlushRequest, FlushResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<FlushResponse> FlushAsync(
			Indices indices,
			Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		) => FlushAsync(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<FlushResponse> FlushAsync(IFlushRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IFlushRequest, FlushResponse>(request, request.RequestParameters, ct);
	}
}
