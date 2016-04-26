using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeletePipelineResponse DeletePipeline( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc/>
		IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request);

		/// <inheritdoc/>
		Task<IDeletePipelineResponse> DeletePipelineAsync( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request);

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
		public Task<IDeletePipelineResponse> DeletePipelineAsync( Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			this.DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor( id)));

		/// <inheritdoc/>
		public Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request) =>
			this.Dispatcher.DispatchAsync<IDeletePipelineRequest, DeletePipelineRequestParameters, DeletePipelineResponse, IDeletePipelineResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IngestDeletePipelineDispatchAsync<DeletePipelineResponse>(p)
			);
	}
}
