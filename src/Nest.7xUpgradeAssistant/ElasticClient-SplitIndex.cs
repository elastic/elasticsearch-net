using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Split(), please update this usage.")]
		public static SplitIndexResponse SplitIndex(this IElasticClient client, IndexName source, IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null
		)
			=> client.Indices.Split(source, target, selector);

		[Obsolete("Moved to client.Indices.Split(), please update this usage.")]
		public static SplitIndexResponse SplitIndex(this IElasticClient client, ISplitIndexRequest request)
			=> client.Indices.Split(request);

		[Obsolete("Moved to client.Indices.SplitAsync(), please update this usage.")]
		public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client,
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SplitAsync(source, target, selector, ct);

		[Obsolete("Moved to client.Indices.SplitAsync(), please update this usage.")]
		public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client, ISplitIndexRequest request, CancellationToken ct = default)
			=> client.Indices.SplitAsync(request, ct);
	}
}
