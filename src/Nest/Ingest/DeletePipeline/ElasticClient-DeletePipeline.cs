using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeletePipelineResponse DeletePipeline( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc/>
		IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request);

		/// <inheritdoc/>
		Task<IDeletePipelineResponse> DeletePipelineAsync( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request) =>
			this.Dispatcher.Dispatch<IDeletePipelineRequest, DeletePipelineRequestParameters, DeletePipelineResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IngestDeletePipelineDispatch<DeletePipelineResponse>(p)
			);

		/// <inheritdoc/>
		public IDeletePipelineResponse DeletePipeline( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			this.DeletePipeline(selector.InvokeOrDefault(new DeletePipelineDescriptor( id)));

		/// <inheritdoc/>
		public Task<IDeletePipelineResponse> DeletePipelineAsync( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,CancellationToken cancellationToken = default(CancellationToken) ) =>
			this.DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor( id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeletePipelineRequest, DeletePipelineRequestParameters, DeletePipelineResponse, IDeletePipelineResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IngestDeletePipelineDispatchAsync<DeletePipelineResponse>(p, c)
			);
	}
}
