using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		Task<ISplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISplitIndexResponse SplitIndex(IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null) =>
			this.SplitIndex(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public ISplitIndexResponse SplitIndex(ISplitIndexRequest request) =>
			this.Dispatcher.Dispatch<ISplitIndexRequest, SplitIndexRequestParameters, SplitIndexResponse>(
				request,
				this.LowLevelDispatch.IndicesSplitDispatch<SplitIndexResponse>
			);

		/// <inheritdoc />
		public Task<ISplitIndexResponse> SplitIndexAsync(
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)) =>
				this.SplitIndexAsync(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public Task<ISplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ISplitIndexRequest, SplitIndexRequestParameters, SplitIndexResponse, ISplitIndexResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IndicesSplitDispatchAsync<SplitIndexResponse>
			);
	}
}
