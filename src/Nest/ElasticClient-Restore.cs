using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

        /// <inheritdoc />
        public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest)
	    {
            restoreRequest.ThrowIfNull("restoreRequest");
	        var observable = new RestoreObservable(this, restoreRequest);
	        return observable;
	    }

        /// <inheritdoc />
        public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> restoreSelector = null)
        {
            restoreSelector.ThrowIfNull("restoreSelector");

            var restoreDescriptor = restoreSelector(new RestoreDescriptor());
            var observable = new RestoreObservable(this, restoreDescriptor);
            return observable;
	    }
	}

    class RestoreObservable : IDisposable, IObservable<IRecoveryStatusResponse>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRestoreRequest _restoreRequest;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);
        private Timer _timer;
        private bool _disposed;
        private string _renamePattern;
        private string _renameReplacement;

        public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest)
        {
            _elasticClient = elasticClient;
            _restoreRequest = restoreRequest;
        }

        public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest, TimeSpan interval)
            : this(elasticClient, restoreRequest)
        {
            interval.ThrowIfNull("interval");
            if (interval.Ticks < 0) throw new ArgumentOutOfRangeException("interval");

            _interval = interval;
        }

        public IDisposable Subscribe(IObserver<IRecoveryStatusResponse> observer)
        {
            observer.ThrowIfNull("observer");

            try
            {
                _restoreRequest.RequestParameters.WaitForCompletion(false);
                var restoreResponse = this._elasticClient.Restore(_restoreRequest);

                _renamePattern = _restoreRequest.RenamePattern;
                _renameReplacement = _restoreRequest.RenameReplacement;

                _timer = new Timer(Restore, observer, _interval.Milliseconds, Timeout.Infinite);
            }
            catch (Exception exception)
            {
                observer.OnError(exception);
            }

            return this;
        }

        private void Restore(object state)
        {
            var observer = state as IObserver<IRecoveryStatusResponse>;

            if (observer == null) throw new ArgumentException("state");

            try
            {
                var watch = new Stopwatch();
                watch.Start();

                var recoveryStatus = _elasticClient.RecoveryStatus(
                    descriptor =>
                        descriptor.Indices(
                            _restoreRequest.Indices.Select(
                                x => Regex.Replace(x.Name, _renamePattern, _renameReplacement)).ToArray())
                            .Detailed(true)
                    );

                if (recoveryStatus.Indices.All(x => x.Value.Shards.All(s => s.Index.Files.Recovered == s.Index.Files.Total)))
                {
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    observer.OnCompleted();
                    return;
                }

               observer.OnNext(recoveryStatus);
                _timer.Change(Math.Max(0, _interval.Milliseconds - watch.ElapsedMilliseconds), Timeout.Infinite);
            }
            catch (Exception exception)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                observer.OnError(exception);
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _timer.Dispose();

            _disposed = true;
        }

        ~RestoreObservable()
        {
            Dispose(false);
        }
    }
}