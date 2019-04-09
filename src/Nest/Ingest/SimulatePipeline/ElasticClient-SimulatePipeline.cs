using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc />
		ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request);

		/// <inheritdoc />
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			SimulatePipeline(selector?.Invoke(new SimulatePipelineDescriptor()));

		public ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request) =>
			Dispatch2<ISimulatePipelineRequest, SimulatePipelineResponse>(request, request.RequestParameters);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		) => SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()), ct);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request, CancellationToken ct = default) =>
			Dispatch2Async<ISimulatePipelineRequest, ISimulatePipelineResponse, SimulatePipelineResponse>(request, request.RequestParameters, ct);
	}
}
