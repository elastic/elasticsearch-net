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
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeletePipelineResponse DeletePipeline(IDeletePipelineRequest request) =>
			DoRequest<IDeletePipelineRequest, DeletePipelineResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public IDeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			DeletePipeline(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)));

		/// <inheritdoc />
		public Task<IDeletePipelineResponse> DeletePipelineAsync(
			Id id,
			Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default
		) => DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeletePipelineRequest, IDeletePipelineResponse, DeletePipelineResponse>(request, request.RequestParameters, ct);
	}
}
