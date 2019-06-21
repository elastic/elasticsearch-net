using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Indices(), please update this usage.")]
		public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client,
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null
		)
			=> client.Cat.Indices(selector);

		[Obsolete("Moved to client.Cat.Indices(), please update this usage.")]
		public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client, ICatIndicesRequest request)
			=> client.Cat.Indices(request);

		[Obsolete("Moved to client.Cat.IndicesAsync(), please update this usage.")]
		public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client,
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.IndicesAsync(selector, ct);

		[Obsolete("Moved to client.Cat.IndicesAsync(), please update this usage.")]
		public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client, ICatIndicesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.IndicesAsync(request, ct);
	}
}
