using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client,
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null
		)
			=> client.Cat.Repositories(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client, ICatRepositoriesRequest request)
			=> client.Cat.Repositories(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client,
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.RepositoriesAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client, ICatRepositoriesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.RepositoriesAsync(request, ct);
	}
}
