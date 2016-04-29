using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ISimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc/>
		ISimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request);

		/// <inheritdoc/>
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc/>
		Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request);

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

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			this.SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()));

		public Task<ISimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request) =>
			this.Dispatcher.DispatchAsync<ISimulatePipelineRequest, SimulatePipelineRequestParameters, SimulatePipelineResponse, ISimulatePipelineResponse>(
				request,
				this.LowLevelDispatch.IngestSimulateDispatchAsync<SimulatePipelineResponse>
			);
	}
}
