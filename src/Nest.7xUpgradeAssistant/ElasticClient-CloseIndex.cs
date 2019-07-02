using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Close(), please update this usage.")]
		public static CloseIndexResponse CloseIndex(this IElasticClient client, Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null
		)
			=> client.Indices.Close(indices, selector);

		[Obsolete("Moved to client.Indices.Close(), please update this usage.")]
		public static CloseIndexResponse CloseIndex(this IElasticClient client, ICloseIndexRequest request)
			=> client.Indices.Close(request);

		[Obsolete("Moved to client.Indices.CloseAsync(), please update this usage.")]
		public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client,
			Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.CloseAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.CloseAsync(), please update this usage.")]
		public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client, ICloseIndexRequest request, CancellationToken ct = default)
			=> client.Indices.CloseAsync(request, ct);
	}
}
