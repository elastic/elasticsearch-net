using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc />
		IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request);

		/// <inheritdoc />
		Task<IDeletePipelineResponse> DeletePipelineAsync(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request) =>
			Dispatcher.Dispatch<IDeletePipelineRequest, DeletePipelineRequestParameters, DeletePipelineResponse>(
				request,
				(p, d) => LowLevelDispatch.IngestDeletePipelineDispatch<DeletePipelineResponse>(p)
			);

		/// <inheritdoc />
		public IDeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			DeletePipeline(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)));

		/// <inheritdoc />
		public Task<IDeletePipelineResponse> DeletePipelineAsync(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeletePipelineRequest, DeletePipelineRequestParameters, DeletePipelineResponse, IDeletePipelineResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IngestDeletePipelineDispatchAsync<DeletePipelineResponse>(p, c)
			);
	}
}
