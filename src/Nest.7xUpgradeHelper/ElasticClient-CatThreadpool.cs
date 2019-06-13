using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatThreadPoolRecord> CatThreadPool(this IElasticClient client,Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatThreadPoolRecord> CatThreadPool(this IElasticClient client,ICatThreadPoolRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(this IElasticClient client,
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(this IElasticClient client,ICatThreadPoolRequest request,
			CancellationToken ct = default
		);
	}

}
