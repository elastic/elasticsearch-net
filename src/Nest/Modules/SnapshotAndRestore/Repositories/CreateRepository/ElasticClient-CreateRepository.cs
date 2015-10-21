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
		IAcknowledgedResponse CreateRepository(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest createRepositoryRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest createRepositoryRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse CreateRepository(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector) =>
			this.CreateRepository(selector?.Invoke(new CreateRepositoryDescriptor(repository)));

		/// <inheritdoc/>
		public IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest request) => 
			this.Dispatcher.Dispatch<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector) => 
			this.CreateRepositoryAsync(selector?.Invoke(new CreateRepositoryDescriptor(repository)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest request) => 
			this.Dispatcher.DispatchAsync<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>
			);
	}
}