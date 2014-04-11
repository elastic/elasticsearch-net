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
		public ISnapshotResponse Snapshot(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotCreateDispatch<SnapshotResponse>(p, d)
			);
		}
		/// <inheritdoc />
		public Task<ISnapshotResponse> SnapshotAsync(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>(p, d)
			);
		}
		/// <inheritdoc />
		public IGetSnapshotResponse GetSnapshot(string repository, string snapshotName, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<GetSnapshotDescriptor, GetSnapshotRequestParameters, GetSnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotGetDispatch<GetSnapshotResponse>(p)
			);
		}
		/// <inheritdoc />
		public Task<IGetSnapshotResponse> GetSnapshotAsync(string repository, string snapshotName, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetSnapshotDescriptor, GetSnapshotRequestParameters, GetSnapshotResponse, IGetSnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotGetDispatchAsync<GetSnapshotResponse>(p)
			);
		}
		/// <inheritdoc />
		public IAcknowledgedResponse DeleteSnapshot(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatch<AcknowledgedResponse>(p)
			);
		}
		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}
		
	}
}