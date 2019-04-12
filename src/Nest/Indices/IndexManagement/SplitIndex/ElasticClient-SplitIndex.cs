using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		ISplitIndexResponse SplitIndex(IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		ISplitIndexResponse SplitIndex(ISplitIndexRequest request);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		Task<ISplitIndexResponse> SplitIndexAsync(
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		Task<ISplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISplitIndexResponse SplitIndex(IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null) =>
			SplitIndex(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public ISplitIndexResponse SplitIndex(ISplitIndexRequest request) =>
			DoRequest<ISplitIndexRequest, SplitIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ISplitIndexResponse> SplitIndexAsync(
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		) =>
			SplitIndexAsync(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public Task<ISplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISplitIndexRequest, ISplitIndexResponse, SplitIndexResponse>(request, request.RequestParameters, ct);
	}
}
