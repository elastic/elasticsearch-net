using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Rollover(), please update this usage.")]
		public static RolloverIndexResponse RolloverIndex(this IElasticClient client, Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null
		)
			=> client.Indices.Rollover(alias, selector);

		[Obsolete("Moved to client.Indices.Rollover(), please update this usage.")]
		public static RolloverIndexResponse RolloverIndex(this IElasticClient client, IRolloverIndexRequest request)
			=> client.Indices.Rollover(request);

		[Obsolete("Moved to client.Indices.RolloverAsync(), please update this usage.")]
		public static Task<RolloverIndexResponse> RolloverIndexAsync(this IElasticClient client,
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.RolloverAsync(alias, selector, ct);

		[Obsolete("Moved to client.Indices.RolloverAsync(), please update this usage.")]
		public static Task<RolloverIndexResponse> RolloverIndexAsync(this IElasticClient client, IRolloverIndexRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.RolloverAsync(request, ct);
	}
}
