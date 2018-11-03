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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			SimulatePipeline(selector?.Invoke(new SimulatePipelineDescriptor()));

		public ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request) =>
			Dispatcher.Dispatch<ISimulatePipelineRequest, SimulatePipelineRequestParameters, SimulatePipelineResponse>(
				request,
				LowLevelDispatch.IngestSimulateDispatch<SimulatePipelineResponse>
			);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()), cancellationToken);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<ISimulatePipelineRequest, SimulatePipelineRequestParameters, SimulatePipelineResponse, ISimulatePipelineResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.IngestSimulateDispatchAsync<SimulatePipelineResponse>
				);
	}
}
