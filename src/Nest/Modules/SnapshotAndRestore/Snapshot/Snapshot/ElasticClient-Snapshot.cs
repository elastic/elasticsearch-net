using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public ISnapshotResponse Snapshot(ISnapshotRequest snapshotRequest)
		{
			return this.Dispatcher.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				snapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotCreateDispatch<SnapshotResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<SnapshotDescriptor, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest snapshotRequest)
		{
			return this.Dispatcher.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				snapshotRequest,
				(p, d) => this.LowLevelDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>(p, d)
			);
		}

	}
}