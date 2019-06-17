using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatThreadPoolRecord> CatThreadPool(this IElasticClient client,
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null
		)
			=> client.Cat.ThreadPool(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatThreadPoolRecord> CatThreadPool(this IElasticClient client, ICatThreadPoolRequest request)
			=> client.Cat.ThreadPool(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(this IElasticClient client,
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.ThreadPoolAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(this IElasticClient client, ICatThreadPoolRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.ThreadPoolAsync(request, ct);
	}
}
