using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Repositories(), please update this usage.")]
		public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client,
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null
		)
			=> client.Cat.Repositories(selector);

		[Obsolete("Moved to client.Cat.Repositories(), please update this usage.")]
		public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client, ICatRepositoriesRequest request)
			=> client.Cat.Repositories(request);

		[Obsolete("Moved to client.Cat.RepositoriesAsync(), please update this usage.")]
		public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client,
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.RepositoriesAsync(selector, ct);

		[Obsolete("Moved to client.Cat.RepositoriesAsync(), please update this usage.")]
		public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client, ICatRepositoriesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.RepositoriesAsync(request, ct);
	}
}
