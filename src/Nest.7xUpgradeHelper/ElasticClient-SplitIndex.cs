using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in
		/// the new index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SplitIndexResponse SplitIndex(this IElasticClient client, IndexName source, IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null
		)
			=> client.Indices.Split(source, target, selector);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in
		/// the new index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SplitIndexResponse SplitIndex(this IElasticClient client, ISplitIndexRequest request)
			=> client.Indices.Split(request);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in
		/// the new index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client,
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.SplitAsync(source, target, selector, ct);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in
		/// the new index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client, ISplitIndexRequest request, CancellationToken ct = default)
			=> client.Indices.SplitAsync(request, ct);
	}
}
