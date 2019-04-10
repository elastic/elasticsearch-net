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
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public IPutPipelineResponse PutPipeline(Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector) =>
			PutPipeline(selector?.Invoke(new PutPipelineDescriptor(id)));

		public IPutPipelineResponse PutPipeline(IPutPipelineRequest request) =>
			DoRequest<IPutPipelineRequest, PutPipelineResponse>(request, request.RequestParameters);

		public Task<IPutPipelineResponse> PutPipelineAsync(
			Id id,
			Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken cancellationToken = default
		) => PutPipelineAsync(selector?.Invoke(new PutPipelineDescriptor(id)), cancellationToken);

		public Task<IPutPipelineResponse> PutPipelineAsync(IPutPipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutPipelineRequest, IPutPipelineResponse, PutPipelineResponse>(request, request.RequestParameters, ct);
	}
}
