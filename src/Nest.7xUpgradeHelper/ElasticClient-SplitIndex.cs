using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		public static SplitIndexResponse SplitIndex(this IElasticClient client,IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		public static SplitIndexResponse SplitIndex(this IElasticClient client,ISplitIndexRequest request);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client,
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		public static Task<SplitIndexResponse> SplitIndexAsync(this IElasticClient client,ISplitIndexRequest request, CancellationToken ct = default);
	}

}
