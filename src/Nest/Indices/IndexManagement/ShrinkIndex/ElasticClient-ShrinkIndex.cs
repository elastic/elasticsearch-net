// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		ShrinkIndexResponse ShrinkIndex(IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null);

		ShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request);

		Task<ShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		Task<ShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public ShrinkIndexResponse ShrinkIndex(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null
		) => ShrinkIndex(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public ShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request) =>
			DoRequest<IShrinkIndexRequest, ShrinkIndexResponse>(request, request.RequestParameters);

		public Task<ShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) => ShrinkIndexAsync(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public Task<ShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IShrinkIndexRequest, ShrinkIndexResponse>(request, request.RequestParameters, ct);
	}
}
