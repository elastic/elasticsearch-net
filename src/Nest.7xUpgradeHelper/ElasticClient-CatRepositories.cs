using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client,Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatRepositoriesRecord> CatRepositories(this IElasticClient client,ICatRepositoriesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client,
			Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(this IElasticClient client,ICatRepositoriesRequest request,
			CancellationToken ct = default
		);
	}

}
