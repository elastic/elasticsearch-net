using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.ForceMerge(), please update this usage.")]
		public static ForceMergeResponse ForceMerge(this IElasticClient client, Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null
		)
			=> client.Indices.ForceMerge(indices, selector);

		[Obsolete("Moved to client.Indices.ForceMerge(), please update this usage.")]
		public static ForceMergeResponse ForceMerge(this IElasticClient client, IForceMergeRequest request)
			=> client.Indices.ForceMerge(request);

		[Obsolete("Moved to client.Indices.ForceMergeAsync(), please update this usage.")]
		public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client, Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ForceMergeAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.ForceMergeAsync(), please update this usage.")]
		public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client, IForceMergeRequest request, CancellationToken ct = default)
			=> client.Indices.ForceMergeAsync(request, ct);
	}
}
