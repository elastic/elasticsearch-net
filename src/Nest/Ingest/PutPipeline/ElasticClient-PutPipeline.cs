using System;
using System.Threading.Tasks;
using Elasticsearch.Net_5_2_0;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc/>
		IPutPipelineResponse PutPipeline(IPutPipelineRequest request);

		/// <inheritdoc/>
		Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));

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

		public Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutPipelineAsync(selector?.Invoke(new PutPipelineDescriptor(id)), cancellationToken);

		public Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutPipelineRequest, PutPipelineRequestParameters, PutPipelineResponse, IPutPipelineResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IngestPutPipelineDispatchAsync<PutPipelineResponse>
			);
	}
}
