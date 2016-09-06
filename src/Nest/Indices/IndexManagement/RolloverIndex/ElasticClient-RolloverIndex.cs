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
		IRolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null);

		IRolloverIndexResponse RolloverIndex(IRolloverIndexRequest request);

		Task<IRolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IRolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null) =>
			this.RolloverIndex(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public IRolloverIndexResponse RolloverIndex(IRolloverIndexRequest request) =>
			this.Dispatcher.Dispatch<IRolloverIndexRequest, RolloverIndexRequestParameters, RolloverIndexResponse>(
				request,
				this.LowLevelDispatch.IndicesRolloverDispatch<RolloverIndexResponse>
			);

		public Task<IRolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.RolloverIndexAsync(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IRolloverIndexRequest, RolloverIndexRequestParameters, RolloverIndexResponse, IRolloverIndexResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IndicesRolloverDispatchAsync<RolloverIndexResponse>
			);
	}
}
