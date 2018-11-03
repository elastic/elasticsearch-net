using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc />
		IPutPipelineResponse PutPipeline(IPutPipelineRequest request);

		/// <inheritdoc />
		Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector) =>
			PutPipeline(selector?.Invoke(new PutPipelineDescriptor(id)));

		public IPutPipelineResponse PutPipeline(IPutPipelineRequest request) =>
			Dispatcher.Dispatch<IPutPipelineRequest, PutPipelineRequestParameters, PutPipelineResponse>(
				request,
				LowLevelDispatch.IngestPutPipelineDispatch<PutPipelineResponse>
			);

		public Task<IPutPipelineResponse> PutPipelineAsync(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutPipelineAsync(selector?.Invoke(new PutPipelineDescriptor(id)), cancellationToken);

		public Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutPipelineRequest, PutPipelineRequestParameters, PutPipelineResponse, IPutPipelineResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IngestPutPipelineDispatchAsync<PutPipelineResponse>
			);
	}
}
