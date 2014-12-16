using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nest
{
    public class SnapshotObservable : IDisposable, IObservable<ISnapshotStatusResponse>
    {
        private readonly IElasticClient _elasticClient;
        private readonly ISnapshotRequest _snapshotRequest;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);
        private Timer _timer;
        private bool _disposed;

        public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
        {
            _elasticClient = elasticClient;
            _snapshotRequest = snapshotRequest;
        }

        public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest, TimeSpan interval)
            :this(elasticClient, snapshotRequest)
        {
            interval.ThrowIfNull("interval");
            if (interval.Ticks < 0) throw new ArgumentOutOfRangeException("interval");

            _interval = interval;
        }

        public IDisposable Subscribe(IObserver<ISnapshotStatusResponse> observer)
        {
            observer.ThrowIfNull("observer");

            try
            {
                _snapshotRequest.RequestParameters.WaitForCompletion(false);
                this._elasticClient.Snapshot(_snapshotRequest);

                _timer = new Timer(Snapshot, observer, _interval.Milliseconds, Timeout.Infinite);
            }
            catch (Exception exception)
            {
                observer.OnError(exception);
            }

            return this;
        }

        private void Snapshot(object state)
        {
            var observer = state as IObserver<ISnapshotStatusResponse>;

            if(observer == null) throw new ArgumentException("state");

            try
            {
                var watch = new Stopwatch();
                watch.Start();

                var snapshotStatusResponse = this._elasticClient.SnapshotStatus(descriptor => descriptor
                    .Repository(_snapshotRequest.Repository)
                    .Snapshot(_snapshotRequest.Snapshot));

                if (!snapshotStatusResponse.IsValid)
                    throw new SnapshotException(snapshotStatusResponse.ConnectionStatus, "Can't create snapshot");

                if (snapshotStatusResponse.Snapshots.All(s => s.ShardsStats.Done == s.ShardsStats.Total))
                {
                    observer.OnCompleted();
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    return;
                }

                observer.OnNext(snapshotStatusResponse);
                _timer.Change(Math.Max(0, _interval.Milliseconds - watch.ElapsedMilliseconds), Timeout.Infinite);
            }
            catch (Exception exception)
            {
                observer.OnError(exception);
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
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

        ~SnapshotObservable()
        {
            Dispose(false);
        }
    }
}