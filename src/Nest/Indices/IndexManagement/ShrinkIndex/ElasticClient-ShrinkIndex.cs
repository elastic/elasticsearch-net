using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IShrinkIndexResponse ShrinkIndex(IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null);

		IShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request);

		Task<IShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		Task<IShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public IShrinkIndexResponse ShrinkIndex(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null
		) => ShrinkIndex(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public IShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request) =>
			Dispatch2<IShrinkIndexRequest, ShrinkIndexResponse>(request, request.RequestParameters);

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) => ShrinkIndexAsync(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IShrinkIndexRequest, IShrinkIndexResponse, ShrinkIndexResponse>(request, request.RequestParameters, ct);
	}
}
