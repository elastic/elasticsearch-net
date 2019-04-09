using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetPipelineResponse GetPipeline(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null);

		/// <inheritdoc />
		IGetPipelineResponse GetPipeline(IGetPipelineRequest request);

		/// <inheritdoc />
		Task<IGetPipelineResponse> GetPipelineAsync(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetPipelineResponse GetPipeline(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null) =>
			GetPipeline(selector.InvokeOrDefault(new GetPipelineDescriptor()));

		/// <inheritdoc />
		public IGetPipelineResponse GetPipeline(IGetPipelineRequest request) =>
			Dispatch2<IGetPipelineRequest, GetPipelineResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetPipelineResponse> GetPipelineAsync(
			Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetPipelineAsync(selector.InvokeOrDefault(new GetPipelineDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetPipelineRequest, IGetPipelineResponse, GetPipelineResponse>(request, request.RequestParameters, ct);
	}
}
