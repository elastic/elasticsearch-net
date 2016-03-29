using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete a repository, if you have ongoing restore operations be sure to delete the indices being restored into first.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name of the repository</param>
		/// <param name="selector">Optionaly provide the delete operation with more details</param>>
		IDeleteRepositoryResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc/>
		IDeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request);

		/// <inheritdoc/>
		Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteRepositoryResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null) =>
			this.DeleteRepository(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc/>
		public IDeleteRepositoryResponse DeleteRepository(IDeleteRepositoryRequest request) => 
			this.Dispatcher.Dispatch<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, DeleteRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteRepositoryDispatch<DeleteRepositoryResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null) => 
			this.DeleteRepositoryAsync(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc/>
		public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, DeleteRepositoryResponse, IDeleteRepositoryResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteRepositoryDispatchAsync<DeleteRepositoryResponse>(p)
			);
	}
}