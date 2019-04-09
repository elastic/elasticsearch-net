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
			CancellationToken cancellationToken = default
		);

		Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public IRolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null) =>
			RolloverIndex(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public IRolloverIndexResponse RolloverIndex(IRolloverIndexRequest request) =>
			Dispatch2<IRolloverIndexRequest, RolloverIndexResponse>(request, request.RequestParameters);

		public Task<IRolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) => RolloverIndexAsync(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public Task<IRolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IRolloverIndexRequest, IRolloverIndexResponse, RolloverIndexResponse>(request, request.RequestParameters, ct);
	}
}
