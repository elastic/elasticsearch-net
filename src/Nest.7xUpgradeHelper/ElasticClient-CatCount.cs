using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatCountRecord> CatCount(this IElasticClient client, Func<CatCountDescriptor, ICatCountRequest> selector = null)
			=> client.Cat.Count(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatCountRecord> CatCount(this IElasticClient client, ICatCountRequest request)
			=> client.Cat.Count(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatCountRecord>> CatCountAsync(this IElasticClient client,
			Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.CountAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatCountRecord>> CatCountAsync(this IElasticClient client, ICatCountRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.CountAsync(request, ct);
	}
}
