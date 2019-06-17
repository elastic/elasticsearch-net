using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ForceMergeResponse ForceMerge(this IElasticClient client, Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null
		)
			=> client.Indices.ForceMerge(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ForceMergeResponse ForceMerge(this IElasticClient client, IForceMergeRequest request)
			=> client.Indices.ForceMerge(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client, Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ForceMergeAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client, IForceMergeRequest request, CancellationToken ct = default)
			=> client.Indices.ForceMergeAsync(request, ct);
	}
}
