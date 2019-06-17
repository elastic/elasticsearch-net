using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Shards(), please update this usage.")]
		public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client, Func<CatShardsDescriptor, ICatShardsRequest> selector = null)
			=> client.Cat.Shards(selector);

		[Obsolete("Moved to client.Cat.Shards(), please update this usage.")]
		public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client, ICatShardsRequest request)
			=> client.Cat.Shards(request);

		[Obsolete("Moved to client.Cat.ShardsAsync(), please update this usage.")]
		public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client,
			Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.ShardsAsync(selector, ct);

		[Obsolete("Moved to client.Cat.ShardsAsync(), please update this usage.")]
		public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client, ICatShardsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.ShardsAsync(request, ct);
	}
}
