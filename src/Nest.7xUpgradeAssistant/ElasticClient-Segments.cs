using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Segments(), please update this usage.")]
		public static SegmentsResponse Segments(this IElasticClient client, Indices indices,
			Func<SegmentsDescriptor, ISegmentsRequest> selector = null
		)
			=> client.Indices.Segments(indices, selector);

		[Obsolete("Moved to client.Indices.Segments(), please update this usage.")]
		public static SegmentsResponse Segments(this IElasticClient client, ISegmentsRequest request)
			=> client.Indices.Segments(request);

		[Obsolete("Moved to client.Indices.SegmentsAsync(), please update this usage.")]
		public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client,
			Indices indices,
			Func<SegmentsDescriptor, ISegmentsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SegmentsAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.SegmentsAsync(), please update this usage.")]
		public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client, ISegmentsRequest request, CancellationToken ct = default)
			=> client.Indices.SegmentsAsync(request, ct);
	}
}
