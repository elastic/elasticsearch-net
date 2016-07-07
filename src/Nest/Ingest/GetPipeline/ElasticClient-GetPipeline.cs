using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{

		/// <inheritdoc/>
		IGetPipelineResponse GetPipeline(Id id, Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null);

		/// <inheritdoc/>
		IGetPipelineResponse GetPipeline(IGetPipelineRequest request);

		/// <inheritdoc/>
		Task<IGetPipelineResponse> GetPipelineAsync(Id id, Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetPipelineResponse GetPipeline( Id id, Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null) =>
			this.GetPipeline(selector.InvokeOrDefault(new GetPipelineDescriptor( id)));

		/// <inheritdoc/>
		public IGetPipelineResponse GetPipeline(IGetPipelineRequest request) =>
			this.Dispatcher.Dispatch<IGetPipelineRequest, GetPipelineRequestParameters, GetPipelineResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IngestGetPipelineDispatch<GetPipelineResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetPipelineResponse> GetPipelineAsync(Id id, Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetPipelineAsync(selector.InvokeOrDefault(new GetPipelineDescriptor( id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetPipelineRequest, GetPipelineRequestParameters, GetPipelineResponse, IGetPipelineResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IngestGetPipelineDispatchAsync<GetPipelineResponse>(p, c)
			);
	}
}
