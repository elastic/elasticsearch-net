using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatSegmentsRecord> CatSegments(this IElasticClient client,
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null
		)
			=> client.Cat.Segments(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatSegmentsRecord> CatSegments(this IElasticClient client, ICatSegmentsRequest request)
			=> client.Cat.Segments(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatSegmentsRecord>> CatSegmentsAsync(this IElasticClient client,
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.SegmentsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatSegmentsRecord>> CatSegmentsAsync(this IElasticClient client, ICatSegmentsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.SegmentsAsync(request, ct);
	}
}
