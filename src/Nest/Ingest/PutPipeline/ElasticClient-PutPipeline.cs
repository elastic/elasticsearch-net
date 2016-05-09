using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc/>
		IPutPipelineResponse PutPipeline(IPutPipelineRequest request);

		/// <inheritdoc/>
		Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc/>
		Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request);

	}
	public partial class ElasticClient
	{
		public IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector) =>
			this.PutPipeline(selector?.Invoke(new PutPipelineDescriptor(id)));

		public IPutPipelineResponse PutPipeline(IPutPipelineRequest request) =>
			this.Dispatcher.Dispatch<IPutPipelineRequest, PutPipelineRequestParameters, PutPipelineResponse>(
				request,
				this.LowLevelDispatch.IngestPutPipelineDispatch<PutPipelineResponse>
			);

		public Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector) =>
			this.PutPipelineAsync(selector?.Invoke(new PutPipelineDescriptor(id)));

		public Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request) =>
			this.Dispatcher.DispatchAsync<IPutPipelineRequest, PutPipelineRequestParameters, PutPipelineResponse, IPutPipelineResponse>(
				request,
				this.LowLevelDispatch.IngestPutPipelineDispatchAsync<PutPipelineResponse>
			);
	}
}
