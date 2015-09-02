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
		/// Before any snapshot or restore operation can be performed a snapshot repository should be registered in Elasticsearch. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name for the repository</param>
		/// <param name="selector">describe what the repository looks like</param>
		IAcknowledgedResponse CreateRepository(string repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest createRepositoryRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateRepositoryAsync(string repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest createRepositoryRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse CreateRepository(string name, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector) => 
			this.Dispatcher.Dispatch<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				selector?.Invoke(new CreateRepositoryDescriptor().Repository(name)),
				(p, d) => this.LowLevelDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p, d.Repository)
			);

		/// <inheritdoc/>
		public IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest request) => 
			this.Dispatcher.Dispatch<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p, d.Repository)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(string name, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector) => 
			this.Dispatcher.DispatchAsync<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				selector?.Invoke(new CreateRepositoryDescriptor().Repository(name)),
				(p, d) => this.LowLevelDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p, d.Repository)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest request) => 
			this.Dispatcher.DispatchAsync<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p, d.Repository)
			);
	}
}