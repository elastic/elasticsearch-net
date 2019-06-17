using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Indices level stats provide statistics on different operations happening on an index. The API provides statistics on
		/// the index level scope (though most stats can also be retrieved using node level scope).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-stats.html
		/// </summary>
		/// <param name="selector">Optionaly further describe the indices stats operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static IndicesShardStoresResponse IndicesShardStores(this IElasticClient client,
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null
		)
			=> client.Indices.ShardStores(AllIndices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static IndicesShardStoresResponse IndicesShardStores(this IElasticClient client, IIndicesShardStoresRequest request)
			=> client.Indices.ShardStores(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<IndicesShardStoresResponse> IndicesShardStoresAsync(this IElasticClient client,
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ShardStoresAsync(AllIndices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<IndicesShardStoresResponse> IndicesShardStoresAsync(this IElasticClient client, IIndicesShardStoresRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.ShardStoresAsync(request, ct);
	}
}
