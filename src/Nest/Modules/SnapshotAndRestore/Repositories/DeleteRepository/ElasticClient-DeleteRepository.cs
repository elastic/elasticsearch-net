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
		/// <summary>
		/// Delete a repository, if you have ongoing restore operations be sure to delete the indices being restored into first.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repositories">The names of the repositories</param>
		/// <param name="selector">Optionaly provide the delete operation with more details</param>
		/// >
		DeleteRepositoryResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc />
		DeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request);

		/// <inheritdoc />
		Task<DeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteRepositoryResponse DeleteRepository(Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null
		) =>
			DeleteRepository(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc />
		public DeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request) =>
			DoRequest<IDeleteRepositoryRequest, DeleteRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteRepositoryResponse> DeleteRepositoryAsync(
			Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRepositoryAsync(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)), ct);

		/// <inheritdoc />
		public Task<DeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRepositoryRequest, DeleteRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
