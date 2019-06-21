using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Shrink(), please update this usage.")]
		public static ShrinkIndexResponse ShrinkIndex(this IElasticClient client, IndexName source, IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null
		)
			=> client.Indices.Shrink(source, target, selector);

		[Obsolete("Moved to client.Indices.Shrink(), please update this usage.")]
		public static ShrinkIndexResponse ShrinkIndex(this IElasticClient client, IShrinkIndexRequest request)
			=> client.Indices.Shrink(request);

		[Obsolete("Moved to client.Indices.ShrinkAsync(), please update this usage.")]
		public static Task<ShrinkIndexResponse> ShrinkIndexAsync(this IElasticClient client,
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ShrinkAsync(source, target, selector, ct);

		[Obsolete("Moved to client.Indices.ShrinkAsync(), please update this usage.")]
		public static Task<ShrinkIndexResponse> ShrinkIndexAsync(this IElasticClient client, IShrinkIndexRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.ShrinkAsync(request, ct);
	}
}
