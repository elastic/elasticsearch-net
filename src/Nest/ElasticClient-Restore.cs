using System;
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
		public IRestoreResponse Restore(IRestoreRequest restoreRequest)
		{
			return this.Dispatch<IRestoreRequest, RestoreRequestParameters, RestoreResponse>(
				restoreRequest,
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<RestoreDescriptor, RestoreRequestParameters, RestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<RestoreResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest)
		{
			return this.DispatchAsync<IRestoreRequest, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				restoreRequest,
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<RestoreDescriptor, RestoreRequestParameters, RestoreResponse, IRestoreResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<RestoreResponse>(p, d)
			);
		}

        public IObservable<IRecoveryStatusResponse> RestoreObservable(IRestoreRequest restoreRequest)
	    {
            restoreRequest.ThrowIfNull("restoreRequest");
	        var observable = new RestoreObservable(this, restoreRequest);
	        return observable;
	    }

        public IObservable<IRecoveryStatusResponse> RestoreObservable(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> restoreSelector = null)
        {
            repository.ThrowIfNull("repository");
            snapshotName.ThrowIfNull("snapshotName");
            restoreSelector.ThrowIfNull("restoreSelector");

            var restoreDescriptor = restoreSelector(new RestoreDescriptor());
            restoreDescriptor.Repository(repository);
            restoreDescriptor.Snapshot(snapshotName);
            var observable = new RestoreObservable(this, restoreDescriptor);
            return observable;
	    }
	}

    class RestoreObservable : IDisposable, IObservable<IRecoveryStatusResponse>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRestoreRequest _restoreRequest;
        private TimeSpan _interval = TimeSpan.FromSeconds(10);

        public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest)
        {
            _elasticClient = elasticClient;
            _restoreRequest = restoreRequest;
        }

        public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest, TimeSpan interval)
            :this(elasticClient, restoreRequest)
        {
            _interval = interval;
        }

        public IDisposable Subscribe(IObserver<IRecoveryStatusResponse> observer)
        {
            observer.ThrowIfNull("observer");

            try
            {
                Restore(observer);
            }
            catch (Exception exception)
            {
                observer.OnError(exception);
            }

            return this;
        }

        private void Restore(IObserver<IRecoveryStatusResponse> observer)
        {
            _restoreRequest.RequestParameters.WaitForCompletion(false);
            this._elasticClient.Restore(_restoreRequest);

            IRecoveryStatusResponse recoveryStatusResponse = null;

            do
            {
                recoveryStatusResponse = _elasticClient.RecoveryStatus(new RecoveryStatusRequest()
                {
                    Detailed = true,
                    Indices = _restoreRequest.Indices
                });

                observer.OnNext(recoveryStatusResponse);
                Thread.Sleep(_interval);
                //TODO: change this 'done' - replace with done < total?
            } while (recoveryStatusResponse.Indices.All(x => x.Value.Shards.All(s => s.Stage != "DONE")));

            observer.OnCompleted();
        }
        //TODO: should I do something here?
        public void Dispose()
        {

        }
    }
}