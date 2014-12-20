using System;
using System.Collections.Generic;
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
        private SnapshotStatusHumbleObject _snapshotStatusHumbleObject;
        private List<EventHandler<NextEventArgs>> _nextEventHandlers = new List<EventHandler<NextEventArgs>>();
        private List<EventHandler<EventArgs>> _completedEentHandlers = new List<EventHandler<EventArgs>>();
        private List<EventHandler<ErrorEventArgs>> _errorEventHandlers = new List<EventHandler<ErrorEventArgs>>(); 

        public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
        {
            elasticClient.ThrowIfNull("elasticClient");
            snapshotRequest.ThrowIfNull("snapshotRequest");

            _elasticClient = elasticClient;
            _snapshotRequest = snapshotRequest;
            _snapshotStatusHumbleObject = new SnapshotStatusHumbleObject(elasticClient, snapshotRequest);
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
                var snapshotResponse = this._elasticClient.Snapshot(_snapshotRequest);

                if (!snapshotResponse.IsValid)
                    throw new SnapshotException(snapshotResponse.ConnectionStatus, "Can't create snapshot");

                EventHandler<NextEventArgs> onNext = (sender, args) => observer.OnNext(args.SnapshotStatusResponse);
                EventHandler<EventArgs> onCompleted = (sender, args) => observer.OnCompleted();
                EventHandler<ErrorEventArgs> onError = (sender, args) => observer.OnError(args.Exception);

                _nextEventHandlers.Add(onNext);
                _completedEentHandlers.Add(onCompleted);
                _errorEventHandlers.Add(onError);

                _snapshotStatusHumbleObject.Next += onNext;
                _snapshotStatusHumbleObject.Completed += onCompleted;
                _snapshotStatusHumbleObject.Error += onError;

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

                _snapshotStatusHumbleObject.Check();

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
            _nextEventHandlers.ForEach(x => _snapshotStatusHumbleObject.Next -= x);
            _completedEentHandlers.ForEach(x => _snapshotStatusHumbleObject.Completed -= x);
            _errorEventHandlers.ForEach(x => _snapshotStatusHumbleObject.Error -= x);

            _disposed = true;
        }

        ~SnapshotObservable()
        {
            Dispose(false);
        }
    }


    public class NextEventArgs : EventArgs
    {
        public ISnapshotStatusResponse SnapshotStatusResponse { get; private set; }

        public NextEventArgs(ISnapshotStatusResponse snapshotStatusResponse)
        {
            SnapshotStatusResponse = snapshotStatusResponse;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

    public class SnapshotStatusHumbleObject
    {
        private readonly IElasticClient _elasticClient;
        private readonly ISnapshotRequest _snapshotRequest;

        public event EventHandler<EventArgs> Completed;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<NextEventArgs> Next;

        public SnapshotStatusHumbleObject(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
        {
            _elasticClient = elasticClient;
            _snapshotRequest = snapshotRequest;
        }

        public void Check()
        {
            try
            {
                var snapshotStatusResponse = this._elasticClient.SnapshotStatus(descriptor => descriptor
                            .Repository(_snapshotRequest.Repository)
                            .Snapshot(_snapshotRequest.Snapshot));

                if (!snapshotStatusResponse.IsValid)
                    throw new SnapshotException(snapshotStatusResponse.ConnectionStatus, "Can't check snapshot status");

                if (snapshotStatusResponse.Snapshots.All(s => s.ShardsStats.Done == s.ShardsStats.Total))
                {
                    OnCompleted();
                    return;
                }

                OnNext(new NextEventArgs(snapshotStatusResponse));
            }
            catch (Exception exception)
            {
                OnError(new ErrorEventArgs(exception));
            }
        }

        protected virtual void OnNext(NextEventArgs nextEventArgs)
        {
            var handler = Next;
            if (handler != null) handler(this, nextEventArgs);
        }

        protected virtual void OnCompleted()
        {
            var handler = Completed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnError(ErrorEventArgs errorEventArgs)
        {
            var handler = Error;
            if (handler != null) handler(this, errorEventArgs);
        }
    }

}