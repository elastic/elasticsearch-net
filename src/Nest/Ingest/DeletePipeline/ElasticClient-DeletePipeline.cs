// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc />
		DeletePipelineResponse DeletePipeline(IDeletePipelineRequest request);

		/// <inheritdoc />
		Task<DeletePipelineResponse> DeletePipelineAsync(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<DeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeletePipelineResponse DeletePipeline(IDeletePipelineRequest request) =>
			DoRequest<IDeletePipelineRequest, DeletePipelineResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public DeletePipelineResponse DeletePipeline(Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null) =>
			DeletePipeline(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)));

		/// <inheritdoc />
		public Task<DeletePipelineResponse> DeletePipelineAsync(
			Id id,
			Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken cancellationToken = default
		) => DeletePipelineAsync(selector.InvokeOrDefault(new DeletePipelineDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<DeletePipelineResponse> DeletePipelineAsync(IDeletePipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeletePipelineRequest, DeletePipelineResponse>(request, request.RequestParameters, ct);
	}
}
