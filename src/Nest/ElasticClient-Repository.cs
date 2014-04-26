using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse CreateRepository(string name, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector)
		{
			name.ThrowIfNullOrEmpty("name");
			return this.Dispatch<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p, d._Repository)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(string name, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector)
		{
			name.ThrowIfNullOrEmpty("name");
			return this.DispatchAsync<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p, d._Repository)
			);
		}
		
		/// <inheritdoc />
		public IAcknowledgedResponse DeleteRepository(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
		}

	}
}