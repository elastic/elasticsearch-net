using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client, Func<CatShardsDescriptor, ICatShardsRequest> selector = null)
			=> client.Cat.Shards(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatShardsRecord> CatShards(this IElasticClient client, ICatShardsRequest request)
			=> client.Cat.Shards(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client,
			Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.ShardsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatShardsRecord>> CatShardsAsync(this IElasticClient client, ICatShardsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.ShardsAsync(request, ct);
	}
}
