// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		SplitIndexResponse SplitIndex(IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		SplitIndexResponse SplitIndex(ISplitIndexRequest request);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		Task<SplitIndexResponse> SplitIndexAsync(
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Split an existing index into a new index, where each original primary shard is split into two or more primary shards in the new index.
		/// </summary>
		Task<SplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SplitIndexResponse SplitIndex(IndexName source, IndexName target, Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null) =>
			SplitIndex(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public SplitIndexResponse SplitIndex(ISplitIndexRequest request) =>
			DoRequest<ISplitIndexRequest, SplitIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SplitIndexResponse> SplitIndexAsync(
			IndexName source,
			IndexName target,
			Func<SplitIndexDescriptor, ISplitIndexRequest> selector = null,
			CancellationToken ct = default
		) =>
			SplitIndexAsync(selector.InvokeOrDefault(new SplitIndexDescriptor(source, target)));

		/// <inheritdoc />
		public Task<SplitIndexResponse> SplitIndexAsync(ISplitIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISplitIndexRequest, SplitIndexResponse>(request, request.RequestParameters, ct);
	}
}
