using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Provide low level segments information that a Lucene index (shard level) is built with.
		/// Allows to be used to provide more information on the state of a shard and an index, possibly optimization information,
		/// data "wasted" on deletes, and so on.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-segments.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the segments operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SegmentsResponse Segments(this IElasticClient client, Indices indices,
			Func<SegmentsDescriptor, ISegmentsRequest> selector = null
		)
			=> client.Indices.Segments(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SegmentsResponse Segments(this IElasticClient client, ISegmentsRequest request)
			=> client.Indices.Segments(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client,
			Indices indices,
			Func<SegmentsDescriptor, ISegmentsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SegmentsAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client, ISegmentsRequest request, CancellationToken ct = default)
			=> client.Indices.SegmentsAsync(request, ct);
	}
}
