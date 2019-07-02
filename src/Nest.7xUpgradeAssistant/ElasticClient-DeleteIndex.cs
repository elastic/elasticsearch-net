using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Delete(), please update this usage.")]
		public static DeleteIndexResponse DeleteIndex(this IElasticClient client, Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null
		)
			=> client.Indices.Delete(indices, selector);

		[Obsolete("Moved to client.Indices.Delete(), please update this usage.")]
		public static DeleteIndexResponse DeleteIndex(this IElasticClient client, IDeleteIndexRequest request)
			=> client.Indices.Delete(request);

		[Obsolete("Moved to client.Indices.DeleteAsync(), please update this usage.")]
		public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client,
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.DeleteAsync(), please update this usage.")]
		public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client, IDeleteIndexRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAsync(request, ct);
	}
}
