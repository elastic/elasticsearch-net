using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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

		Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public IRolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null) =>
			RolloverIndex(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public IRolloverIndexResponse RolloverIndex(IRolloverIndexRequest request) =>
			Dispatcher.Dispatch<IRolloverIndexRequest, RolloverIndexRequestParameters, RolloverIndexResponse>(
				request,
				LowLevelDispatch.IndicesRolloverDispatch<RolloverIndexResponse>
			);

		public Task<IRolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => RolloverIndexAsync(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRolloverIndexRequest, RolloverIndexRequestParameters, RolloverIndexResponse, IRolloverIndexResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IndicesRolloverDispatchAsync<RolloverIndexResponse>
			);
	}
}
