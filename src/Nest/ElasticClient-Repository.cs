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

		public ISnapshotResponse Snapshot(string name, string repository, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotCreateDispatch<SnapshotResponse>(p, d)
			);
		}
		public Task<ISnapshotResponse> SnapshotAsync(string name, string repository, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>(p, d)
			);
		}
		public IGetSnapshotResponse GetSnapshot(string name, string repository, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<GetSnapshotDescriptor, GetSnapshotRequestParameters, GetSnapshotResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotGetDispatch<GetSnapshotResponse>(p)
			);
		}
		public Task<IGetSnapshotResponse> GetSnapshotAsync(string name, string repository, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetSnapshotDescriptor, GetSnapshotRequestParameters, GetSnapshotResponse, IGetSnapshotResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotGetDispatchAsync<GetSnapshotResponse>(p)
			);
		}
		public IAcknowledgedResponse DeleteSnapshot(string name, string repository, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatch<AcknowledgedResponse>(p)
			);
		}
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(string name, string repository, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}
		public IAcknowledgedResponse Restore(string name, string repository, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<RestoreDescriptor, RestoreRequestParameters, AcknowledgedResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<AcknowledgedResponse>(p, d)
			);
		}
		public Task<IAcknowledgedResponse> RestoreAsync(string name, string repository, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<RestoreDescriptor, RestoreRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(name).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<AcknowledgedResponse>(p, d)
			);
		}
	}
}