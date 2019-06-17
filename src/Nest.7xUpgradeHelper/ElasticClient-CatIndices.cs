using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client,
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null
		)
			=> client.Cat.Indices(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client, ICatIndicesRequest request)
			=> client.Cat.Indices(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client,
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.IndicesAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client, ICatIndicesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.IndicesAsync(request, ct);
	}
}
