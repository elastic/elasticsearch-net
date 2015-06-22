using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse DeleteSnapshot(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse DeleteSnapshot(IDeleteSnapshotRequest deleteSnapshotRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
				deleteSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest deleteSnapshotRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				deleteSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}
	}
}