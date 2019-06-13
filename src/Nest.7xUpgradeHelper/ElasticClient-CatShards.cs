using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client,Func<CatShardsDescriptor, ICatShardsRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client,ICatShardsRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client,Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client,ICatShardsRequest request, CancellationToken ct = default
		);
	}

}
