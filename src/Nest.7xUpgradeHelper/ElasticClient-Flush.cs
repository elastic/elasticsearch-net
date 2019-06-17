using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Flush(), please update this usage.")]
		public static FlushResponse Flush(this IElasticClient client, Indices indices, Func<FlushDescriptor, IFlushRequest> selector = null)
			=> client.Indices.Flush(indices, selector);

		[Obsolete("Moved to client.Indices.Flush(), please update this usage.")]
		public static FlushResponse Flush(this IElasticClient client, IFlushRequest request)
			=> client.Indices.Flush(request);

		[Obsolete("Moved to client.Indices.FlushAsync(), please update this usage.")]
		public static Task<FlushResponse> FlushAsync(this IElasticClient client, Indices indices,
			Func<FlushDescriptor, IFlushRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.FlushAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.FlushAsync(), please update this usage.")]
		public static Task<FlushResponse> FlushAsync(this IElasticClient client, IFlushRequest request, CancellationToken ct = default)
			=> client.Indices.FlushAsync(request, ct);
	}
}
