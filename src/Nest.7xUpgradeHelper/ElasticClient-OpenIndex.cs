using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Open(), please update this usage.")]
		public static OpenIndexResponse OpenIndex(this IElasticClient client, Indices indices,
			Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null
		)
			=> client.Indices.Open(indices, selector);

		[Obsolete("Moved to client.Indices.Open(), please update this usage.")]
		public static OpenIndexResponse OpenIndex(this IElasticClient client, IOpenIndexRequest request)
			=> client.Indices.Open(request);

		[Obsolete("Moved to client.Indices.OpenAsync(), please update this usage.")]
		public static Task<OpenIndexResponse> OpenIndexAsync(this IElasticClient client,
			Indices indices,
			Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.OpenAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.OpenAsync(), please update this usage.")]
		public static Task<OpenIndexResponse> OpenIndexAsync(this IElasticClient client, IOpenIndexRequest request, CancellationToken ct = default)
			=> client.Indices.OpenAsync(request, ct);
	}
}
