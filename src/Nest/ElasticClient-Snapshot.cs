using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public IObservable<ISnapshotStatusResponse> SnapshotObservable(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null)
        {
            repository.ThrowIfNull("repository");
            snapshotName.ThrowIfNull("snapshotName");
            snapshotSelector.ThrowIfNull("snapshotSelector");

            var snapshotDescriptor = snapshotSelector(new SnapshotDescriptor());
            snapshotDescriptor.Repository(repository);
            snapshotDescriptor.Snapshot(snapshotName);
            var observable = new SnapshotObservable(this, snapshotDescriptor);
            return observable;
        }

	    /// <inheritdoc />
        public IObservable<ISnapshotStatusResponse> SnapshotObservable(ISnapshotRequest snapshotRequest)
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

    public class SnapshotObservable : IDisposable, IObservable<ISnapshotStatusResponse>
    {
        private readonly IElasticClient _elasticClient;
        private readonly ISnapshotRequest _snapshotRequest;
        private TimeSpan _interval = TimeSpan.FromSeconds(10);

        public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
        {
            _elasticClient = elasticClient;
            _snapshotRequest = snapshotRequest;
        }

        public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest, TimeSpan interval)
            :this(elasticClient, snapshotRequest)
        {
            _interval = interval;
        }

        public IDisposable Subscribe(IObserver<ISnapshotStatusResponse> observer)
        {
            observer.ThrowIfNull("observer");
            try
            {
                this.Snapshot(observer);
            }
            catch (Exception exception)
            {
                observer.OnError(exception);
            }

            return this;
        }

        private void Snapshot(IObserver<ISnapshotStatusResponse> observer)
        {
            _snapshotRequest.RequestParameters.WaitForCompletion(false);
            this._elasticClient.Snapshot(_snapshotRequest);

            ISnapshotStatusResponse snapshotStatusResponse;

            do
            {
                snapshotStatusResponse = this._elasticClient.SnapshotStatus(descriptor => descriptor
                    .Repository(_snapshotRequest.Repository)
                    .Snapshot(_snapshotRequest.Snapshot));
                if (!snapshotStatusResponse.IsValid)
                    throw new SnapshotException(snapshotStatusResponse.ConnectionStatus, "Can't create  snapshot");
                
                observer.OnNext(new SnapshotStatusResponse
                {
                    IsValid = true,
                    Snapshots = snapshotStatusResponse.Snapshots
                });
                Thread.Sleep(_interval);
                //TODO: change plain text to somethin else - replace to done < total?
            } while (snapshotStatusResponse.Snapshots.All(s => s.State != "SUCCESS"));
 
            observer.OnCompleted();
        }

        //TODO: should I do something here?
        public void Dispose()
        { 

        }
    }

    public class Observer<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _completed;

        public Observer(Action<T> onNext = null, Action<Exception> onError = null, Action completed = null)
        {
            _onNext = onNext;
            _onError = onError;
            _completed = completed;
        }

        public void OnNext(T value)
        {
            if (this._onNext != null) this._onNext(value);
        }

        public void OnError(Exception error)
        {
            if (this._onError != null) this._onError(error);
        }

        public void OnCompleted()
        {
            if (this._completed != null) this._completed();
        }
    }
}