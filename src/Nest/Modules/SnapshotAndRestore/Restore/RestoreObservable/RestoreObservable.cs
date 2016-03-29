using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Elasticsearch.Net;

namespace Nest
{
	public class RestoreObservable : IDisposable, IObservable<IRecoveryStatusResponse>
	{
		private readonly IElasticClient _elasticClient;
		private readonly IRestoreRequest _restoreRequest;
		private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);
		private Timer _timer;
		private bool _disposed;
		private readonly RestoreStatusHumbleObject _restoreStatusHumbleObject;
		private EventHandler<RestoreNextEventArgs> _nextEventHandlers;
		private EventHandler<RestoreCompletedEventArgs> _completedEentHandlers;
		private EventHandler<RestoreErrorEventArgs> _errorEventHandlers;

		public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest)
		{
			elasticClient.ThrowIfNull(nameof(elasticClient));
			restoreRequest.ThrowIfNull(nameof(restoreRequest));

			_elasticClient = elasticClient;
			_restoreRequest = restoreRequest;

			_restoreStatusHumbleObject = new RestoreStatusHumbleObject(elasticClient, restoreRequest);
			_restoreStatusHumbleObject.Completed += StopTimer;
			_restoreStatusHumbleObject.Error += StopTimer;
		}

		public RestoreObservable(IElasticClient elasticClient, IRestoreRequest restoreRequest, TimeSpan interval)
			: this(elasticClient, restoreRequest)
		{
			interval.ThrowIfNull(nameof(interval));
			if (interval.Ticks < 0) throw new ArgumentOutOfRangeException(nameof(interval));

			_interval = interval;
		}

		public IDisposable Subscribe(IObserver<IRecoveryStatusResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));

			try
			{
				_restoreRequest.RequestParameters.WaitForCompletion(false);
				var restoreResponse = this._elasticClient.Restore(_restoreRequest);

				if (!restoreResponse.IsValid)
					throw new ElasticsearchClientException(PipelineFailure.BadResponse, "Failed to restore snapshot.", restoreResponse.ApiCall);

				EventHandler<RestoreNextEventArgs> onNext = (sender, args) => observer.OnNext(args.RecoveryStatusResponse);
				EventHandler<RestoreCompletedEventArgs> onCompleted = (sender, args) => observer.OnCompleted();
				EventHandler<RestoreErrorEventArgs> onError = (sender, args) => observer.OnError(args.Exception);

				_nextEventHandlers = onNext;
				_completedEentHandlers = onCompleted;
				_errorEventHandlers = onError;

				_restoreStatusHumbleObject.Next += onNext;
				_restoreStatusHumbleObject.Completed += onCompleted;
				_restoreStatusHumbleObject.Error += onError;

				_timer = new Timer(Restore, observer, _interval, Timeout.InfiniteTimeSpan);
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

			if (observer == null) throw new ArgumentException($"must be an {nameof(IObserver<IRecoveryStatusResponse>)}", nameof(state));

			try
			{
				var watch = new Stopwatch();
				watch.Start();

				_restoreStatusHumbleObject.CheckStatus();

				_timer.Change(TimeSpan.FromMilliseconds(Math.Max(0, _interval.TotalMilliseconds - watch.ElapsedMilliseconds)), Timeout.InfiniteTimeSpan);
			}
			catch (Exception exception)
			{
				observer.OnError(exception);
			}
		}

		private void StopTimer(object sender, EventArgs restoreCompletedEventArgs)
		{
			_timer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_timer?.Dispose();

			if (_restoreStatusHumbleObject != null)
			{
				_restoreStatusHumbleObject.Next -= _nextEventHandlers;
				_restoreStatusHumbleObject.Completed -= _completedEentHandlers;
				_restoreStatusHumbleObject.Error -= _errorEventHandlers;

				_restoreStatusHumbleObject.Completed -= StopTimer;
				_restoreStatusHumbleObject.Error -= StopTimer;
			}

			_disposed = true;
		}

		~RestoreObservable()
		{
			Dispose(false);
		}
	}

	public class RestoreNextEventArgs : EventArgs
	{
		public IRecoveryStatusResponse RecoveryStatusResponse { get; }

		public RestoreNextEventArgs(IRecoveryStatusResponse recoveryStatusResponse)
		{
			RecoveryStatusResponse = recoveryStatusResponse;
		}
	}

	public class RestoreCompletedEventArgs : EventArgs
	{
		public IRecoveryStatusResponse RecoveryStatusResponse { get; }

		public RestoreCompletedEventArgs(IRecoveryStatusResponse recoveryStatusResponse)
		{
			RecoveryStatusResponse = recoveryStatusResponse;
		}
	}

	public class RestoreErrorEventArgs : EventArgs
	{
		public Exception Exception { get; }

		public RestoreErrorEventArgs(Exception exception)
		{
			Exception = exception;
		}
	}

	public class RestoreStatusHumbleObject
	{
		private readonly IElasticClient _elasticClient;
		private readonly IRestoreRequest _restoreRequest;
		private readonly string _renamePattern;
		private readonly string _renameReplacement;

		public event EventHandler<RestoreCompletedEventArgs> Completed;
		public event EventHandler<RestoreErrorEventArgs> Error;
		public event EventHandler<RestoreNextEventArgs> Next;

		public RestoreStatusHumbleObject(IElasticClient elasticClient, IRestoreRequest restoreRequest)
		{
			elasticClient.ThrowIfNull(nameof(elasticClient));
			restoreRequest.ThrowIfNull(nameof(restoreRequest));

			_elasticClient = elasticClient;
			_restoreRequest = restoreRequest;

			_renamePattern = string.IsNullOrEmpty(_restoreRequest.RenamePattern) ? string.Empty : _restoreRequest.RenamePattern;
			_renameReplacement = string.IsNullOrEmpty(_restoreRequest.RenameReplacement) ? string.Empty : _restoreRequest.RenameReplacement;
		}

		public void CheckStatus()
		{
			try
			{
				var indices =
					_restoreRequest.Indices.Item2.Indices.Select(
						x => new IndexName
						{
							Name = Regex.Replace(x.Name, _renamePattern, _renameReplacement),
							Type = x.Type
						})
						.ToArray();

				var recoveryStatus = _elasticClient.RecoveryStatus(new RecoveryStatusRequest(indices)
				{
					Detailed = true,
				});

				if (!recoveryStatus.IsValid)
					throw new ElasticsearchClientException(PipelineFailure.BadResponse, "Failed getting recovery status.", recoveryStatus.ApiCall);

				if (recoveryStatus.Indices.All(x => x.Value.Shards.All(s => s.Index.Files.Recovered == s.Index.Files.Total)))
				{
					OnCompleted(new RestoreCompletedEventArgs(recoveryStatus));
					return;
				}

				OnNext(new RestoreNextEventArgs(recoveryStatus));
			}
			catch (Exception exception)
			{
				OnError(new RestoreErrorEventArgs(exception));
			}
		}

		protected virtual void OnNext(RestoreNextEventArgs nextEventArgs)
		{
			var handler = Next;
			handler?.Invoke(this, nextEventArgs);
		}

		protected virtual void OnCompleted(RestoreCompletedEventArgs completedEventArgs)
		{
			var handler = Completed;
			handler?.Invoke(this, completedEventArgs);
		}

		protected virtual void OnError(RestoreErrorEventArgs errorEventArgs)
		{
			var handler = Error;
			handler?.Invoke(this, errorEventArgs);
		}
	}
}