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
		IFlushResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null);

		/// <inheritdoc />
		IFlushResponse Flush(IFlushRequest request);

		/// <inheritdoc />
		Task<IFlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IFlushResponse> FlushAsync(IFlushRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IFlushResponse Flush(Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null) =>
			Flush(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)));

		/// <inheritdoc />
		public IFlushResponse Flush(IFlushRequest request) =>
			Dispatch2<IFlushRequest, FlushResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IFlushResponse> FlushAsync(
			Indices indices,
			Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		) => FlushAsync(selector.InvokeOrDefault(new FlushDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<IFlushResponse> FlushAsync(IFlushRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IFlushRequest, IFlushResponse, FlushResponse>(request, request.RequestParameters, ct);
	}
}
