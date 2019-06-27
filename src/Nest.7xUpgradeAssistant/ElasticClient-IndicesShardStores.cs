using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.ShardStores(), please update this usage.")]
		public static IndicesShardStoresResponse IndicesShardStores(this IElasticClient client,
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null
		)
			=> client.Indices.ShardStores(AllIndices, selector);

		[Obsolete("Moved to client.Indices.ShardStores(), please update this usage.")]
		public static IndicesShardStoresResponse IndicesShardStores(this IElasticClient client, IIndicesShardStoresRequest request)
			=> client.Indices.ShardStores(request);

		[Obsolete("Moved to client.Indices.ShardStoresAsync(), please update this usage.")]
		public static Task<IndicesShardStoresResponse> IndicesShardStoresAsync(this IElasticClient client,
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ShardStoresAsync(AllIndices, selector, ct);

		[Obsolete("Moved to client.Indices.ShardStoresAsync(), please update this usage.")]
		public static Task<IndicesShardStoresResponse> IndicesShardStoresAsync(this IElasticClient client, IIndicesShardStoresRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.ShardStoresAsync(request, ct);
	}
}
