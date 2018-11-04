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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetPipelineResponse GetPipeline(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null) =>
			GetPipeline(selector.InvokeOrDefault(new GetPipelineDescriptor()));

		/// <inheritdoc />
		public IGetPipelineResponse GetPipeline(IGetPipelineRequest request) =>
			Dispatcher.Dispatch<IGetPipelineRequest, GetPipelineRequestParameters, GetPipelineResponse>(
				request,
				(p, d) => LowLevelDispatch.IngestGetPipelineDispatch<GetPipelineResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetPipelineResponse> GetPipelineAsync(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetPipelineAsync(selector.InvokeOrDefault(new GetPipelineDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetPipelineRequest, GetPipelineRequestParameters, GetPipelineResponse, IGetPipelineResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IngestGetPipelineDispatchAsync<GetPipelineResponse>(p, c)
			);
	}
}
