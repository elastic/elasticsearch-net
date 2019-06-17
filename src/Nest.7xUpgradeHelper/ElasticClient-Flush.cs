using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
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
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FlushResponse Flush(this IElasticClient client, Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null)
			=> client.Indices.Flush(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FlushResponse Flush(this IElasticClient client, IFlushRequest request)
			=> client.Indices.Flush(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FlushResponse> FlushAsync(this IElasticClient client, Indices indices,
			Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.FlushAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FlushResponse> FlushAsync(this IElasticClient client, IFlushRequest request, CancellationToken ct = default)
			=> client.Indices.FlushAsync(request, ct);
	}
}
