using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
	
		/// <inheritdoc />
		public IAcknowledgedResponse DeleteRepository(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse DeleteRepository(IDeleteRepositoryRequest deleteRepositoryRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				deleteRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest deleteRepositoryRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				deleteRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
		}
	}
}