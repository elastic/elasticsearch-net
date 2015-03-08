using System;
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
		public ISnapshotResponse Snapshot(ISnapshotRequest snapshotRequest)
		{
			return this.Dispatch<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse>(
				snapshotRequest,
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
		public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest snapshotRequest)
		{
			return this.DispatchAsync<ISnapshotRequest, SnapshotRequestParameters, SnapshotResponse, ISnapshotResponse>(
				snapshotRequest,
				(p, d) => this.RawDispatch.SnapshotCreateDispatchAsync<SnapshotResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector.ThrowIfNull("snapshotSelector");

			var snapshotDescriptor = snapshotSelector(new SnapshotDescriptor());
			var observable = new SnapshotObservable(this, snapshotDescriptor);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest snapshotRequest)
		{
			snapshotRequest.ThrowIfNull("snapshotRequest");
			var observable = new SnapshotObservable(this, snapshotRequest);
			return observable;
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
		public IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest getSnapshotRequest)
		{
			return this.Dispatch<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse>(
				getSnapshotRequest,
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
		public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest getSnapshotRequest)
		{
			return this.DispatchAsync<IGetSnapshotRequest, GetSnapshotRequestParameters, GetSnapshotResponse, IGetSnapshotResponse>(
				getSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotGetDispatchAsync<GetSnapshotResponse>(p)
			);
		}


		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null)
		{
			return this.Dispatch<SnapshotStatusDescriptor, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest getSnapshotRequest)
		{
			return this.Dispatch<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse>(
				getSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotStatusDispatch<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null)
		{
			return this.DispatchAsync<SnapshotStatusDescriptor, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest getSnapshotRequest)
		{
			return this.DispatchAsync<ISnapshotStatusRequest, SnapshotStatusRequestParameters, SnapshotStatusResponse, ISnapshotStatusResponse>(
				getSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotStatusDispatchAsync<SnapshotStatusResponse>(p)
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
		public IAcknowledgedResponse DeleteSnapshot(IDeleteSnapshotRequest deleteSnapshotRequest)
		{
			return this.Dispatch<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse>(
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
			return this.DispatchAsync<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest deleteSnapshotRequest)
		{
			return this.DispatchAsync<IDeleteSnapshotRequest, DeleteSnapshotRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				deleteSnapshotRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteDispatchAsync<AcknowledgedResponse>(p)
			);
		}
	}
}