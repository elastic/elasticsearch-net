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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		Task<IShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IShrinkIndexResponse ShrinkIndex(IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null
		) =>
			ShrinkIndex(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public IShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request) =>
			Dispatcher.Dispatch<IShrinkIndexRequest, ShrinkIndexRequestParameters, ShrinkIndexResponse>(
				request,
				LowLevelDispatch.IndicesShrinkDispatch<ShrinkIndexResponse>
			);

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => ShrinkIndexAsync(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IShrinkIndexRequest, ShrinkIndexRequestParameters, ShrinkIndexResponse, IShrinkIndexResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IndicesShrinkDispatchAsync<ShrinkIndexResponse>
			);
	}
}
