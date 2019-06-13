using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatCountRecord> CatCount(this IElasticClient client,Func<CatCountDescriptor, ICatCountRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatCountRecord> CatCount(this IElasticClient client,ICatCountRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatCountRecord>> CatCountAsync(this IElasticClient client,Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatCountRecord>> CatCountAsync(this IElasticClient client,ICatCountRequest request, CancellationToken ct = default);
	}
}
