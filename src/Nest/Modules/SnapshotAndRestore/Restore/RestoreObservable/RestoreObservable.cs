// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Elastic.Transport;

namespace Nest
{
	public class RestoreObservable : IDisposable, IObservable<RecoveryStatusResponse>
	{
		private readonly IElasticClient _elasticClient;
		private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);
		private readonly IRestoreRequest _restoreRequest;
		private readonly RestoreStatusHumbleObject _restoreStatusHumbleObject;
		private EventHandler<RestoreCompletedEventArgs> _completedEentHandlers;
		private bool _disposed;
		private EventHandler<RestoreErrorEventArgs> _errorEventHandlers;
		private EventHandler<RestoreNextEventArgs> _nextEventHandlers;
		private Timer _timer;

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

		public void Dispose() => Dispose(true);

		public IDisposable Subscribe(IObserver<RecoveryStatusResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));

			try
			{
				_restoreRequest.RequestParameters.WaitForCompletion = false;
				var restoreResponse = _elasticClient.Snapshot.Restore(_restoreRequest);

				if (!restoreResponse.IsValid)
					throw new TransportException(PipelineFailure.BadResponse, "Failed to restore snapshot.", restoreResponse.ApiCall);

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
			var observer = state as IObserver<RecoveryStatusResponse>;

			if (observer == null) throw new ArgumentException($"must be an {nameof(IObserver<RecoveryStatusResponse>)}", nameof(state));

			try
			{
				var watch = new Stopwatch();
				watch.Start();

				_restoreStatusHumbleObject.CheckStatus();

				_timer.Change(TimeSpan.FromMilliseconds(Math.Max(0, _interval.TotalMilliseconds - watch.ElapsedMilliseconds)),
					Timeout.InfiniteTimeSpan);
			}
			catch (Exception exception)
			{
				observer.OnError(exception);
			}
		}

		private void StopTimer(object sender, EventArgs restoreCompletedEventArgs) => _timer.Change(Timeout.Infinite, Timeout.Infinite);

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

		~RestoreObservable() => Dispose(false);
	}

	public class RestoreNextEventArgs : EventArgs
	{
		public RestoreNextEventArgs(RecoveryStatusResponse recoveryStatusResponse) => RecoveryStatusResponse = recoveryStatusResponse;

		public RecoveryStatusResponse RecoveryStatusResponse { get; }
	}

	public class RestoreCompletedEventArgs : EventArgs
	{
		public RestoreCompletedEventArgs(RecoveryStatusResponse recoveryStatusResponse) => RecoveryStatusResponse = recoveryStatusResponse;

		public RecoveryStatusResponse RecoveryStatusResponse { get; }
	}

	public class RestoreErrorEventArgs : EventArgs
	{
		public RestoreErrorEventArgs(Exception exception) => Exception = exception;

		public Exception Exception { get; }
	}

	public class RestoreStatusHumbleObject
	{
		private readonly IElasticClient _elasticClient;
		private readonly string _renamePattern;
		private readonly string _renameReplacement;
		private readonly IRestoreRequest _restoreRequest;

		public RestoreStatusHumbleObject(IElasticClient elasticClient, IRestoreRequest restoreRequest)
		{
			elasticClient.ThrowIfNull(nameof(elasticClient));
			restoreRequest.ThrowIfNull(nameof(restoreRequest));

			_elasticClient = elasticClient;
			_restoreRequest = restoreRequest;

			_renamePattern = string.IsNullOrEmpty(_restoreRequest.RenamePattern) ? string.Empty : _restoreRequest.RenamePattern;
			_renameReplacement = string.IsNullOrEmpty(_restoreRequest.RenameReplacement) ? string.Empty : _restoreRequest.RenameReplacement;
		}

		public event EventHandler<RestoreCompletedEventArgs> Completed;
		public event EventHandler<RestoreErrorEventArgs> Error;
		public event EventHandler<RestoreNextEventArgs> Next;

		public void CheckStatus()
		{
			try
			{
				var indices =
					_restoreRequest.Indices.Item2.Indices.Select(
							x => IndexName.Rebuild(
								Regex.Replace(x.Name, _renamePattern, _renameReplacement),
								x.Type
							))
						.ToArray();

				var recoveryStatus = _elasticClient.Indices.RecoveryStatus(new RecoveryStatusRequest(indices)
				{
					Detailed = true,
				});

				if (!recoveryStatus.IsValid)
					throw new TransportException(PipelineFailure.BadResponse, "Failed getting recovery status.", recoveryStatus.ApiCall);

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
