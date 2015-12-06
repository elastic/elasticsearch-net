using System;
using System.Collections.Generic;
using System.Linq;
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
		IAcknowledgedResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteRepository(IDeleteRepositoryRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteRepository(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null) =>
			this.DeleteRepository(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteRepository(IDeleteRepositoryRequest request) => 
			this.Dispatcher.Dispatch<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null) => 
			this.DeleteRepositoryAsync(selector.InvokeOrDefault(new DeleteRepositoryDescriptor(repositories)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}