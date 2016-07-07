using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc/>
		ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request);

		/// <inheritdoc/>
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		public ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			this.SimulatePipeline(selector?.Invoke(new SimulatePipelineDescriptor()));

		public ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request) =>
			this.Dispatcher.Dispatch<ISimulatePipelineRequest, SimulatePipelineRequestParameters, SimulatePipelineResponse>(
				request,
				this.LowLevelDispatch.IngestSimulateDispatch<SimulatePipelineResponse>
			);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()), cancellationToken);

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ISimulatePipelineRequest, SimulatePipelineRequestParameters, SimulatePipelineResponse, ISimulatePipelineResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IngestSimulateDispatchAsync<SimulatePipelineResponse>
			);
	}
}
