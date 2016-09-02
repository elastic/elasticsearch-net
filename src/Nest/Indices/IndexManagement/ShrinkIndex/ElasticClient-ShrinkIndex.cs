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
		public IShrinkIndexResponse ShrinkIndex(IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null) =>
			this.ShrinkIndex(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public IShrinkIndexResponse ShrinkIndex(IShrinkIndexRequest request) =>
			this.Dispatcher.Dispatch<IShrinkIndexRequest, ShrinkIndexRequestParameters, ShrinkIndexResponse>(
				request,
				this.LowLevelDispatch.IndicesShrinkDispatch<ShrinkIndexResponse>
			);

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.ShrinkIndexAsync(selector.InvokeOrDefault(new ShrinkIndexDescriptor(source, target)));

		public Task<IShrinkIndexResponse> ShrinkIndexAsync(IShrinkIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IShrinkIndexRequest, ShrinkIndexRequestParameters, ShrinkIndexResponse, IShrinkIndexResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IndicesShrinkDispatchAsync<ShrinkIndexResponse>
			);
	}
}
