using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		IDeleteRepositoryResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc />
		IDeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request);

		/// <inheritdoc />
		Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteRepositoryResponse DeleteRepository(Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null
		) =>
			DeleteRepository(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc />
		public IDeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request) =>
			DoRequest<IDeleteRepositoryRequest, DeleteRepositoryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(
			Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRepositoryAsync(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)), ct);

		/// <inheritdoc />
		public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRepositoryRequest, IDeleteRepositoryResponse, DeleteRepositoryResponse>(request, request.RequestParameters, ct);
	}
}
