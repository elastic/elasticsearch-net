/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;

namespace Nest
{
	public class SnapshotObservable : IDisposable, IObservable<SnapshotStatusResponse>
	{
		private readonly IElasticClient _elasticClient;
		private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);
		private readonly ISnapshotRequest _snapshotRequest;
		private readonly SnapshotStatusHumbleObject _snapshotStatusHumbleObject;
		private EventHandler<SnapshotCompletedEventArgs> _completedEventHandler;
		private bool _disposed;
		private EventHandler<SnapshotErrorEventArgs> _errorEventHandler;
		private EventHandler<SnapshotNextEventArgs> _nextEventHandler;
		private Timer _timer;

		public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
		{
			elasticClient.ThrowIfNull(nameof(elasticClient));
			snapshotRequest.ThrowIfNull(nameof(snapshotRequest));

			_elasticClient = elasticClient;
			_snapshotRequest = snapshotRequest;
			_snapshotRequest.RequestParameters.SetRequestMetaData(RequestMetaDataFactory.SnapshotHelperRequestMetaData());
			_snapshotStatusHumbleObject = new SnapshotStatusHumbleObject(elasticClient, snapshotRequest);
			_snapshotStatusHumbleObject.Completed += StopTimer;
			_snapshotStatusHumbleObject.Error += StopTimer;
		}

		public SnapshotObservable(IElasticClient elasticClient, ISnapshotRequest snapshotRequest, TimeSpan interval)
			: this(elasticClient, snapshotRequest)
		{
			interval.ThrowIfNull(nameof(interval));
			if (interval.Ticks < 0) throw new ArgumentOutOfRangeException(nameof(interval));

			_interval = interval;
		}

		public void Dispose() => Dispose(true);

		public IDisposable Subscribe(IObserver<SnapshotStatusResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));

			try
			{
				_snapshotRequest.RequestParameters.WaitForCompletion = false;
				var snapshotResponse = _elasticClient.Snapshot.Snapshot(_snapshotRequest);

				if (!snapshotResponse.IsValid)
					throw new ElasticsearchClientException(PipelineFailure.BadResponse, "Failed to create snapshot.", snapshotResponse.ApiCall);

				EventHandler<SnapshotNextEventArgs> onNext = (sender, args) => observer.OnNext(args.SnapshotStatusResponse);
				EventHandler<SnapshotCompletedEventArgs> onCompleted = (sender, args) => observer.OnCompleted();
				EventHandler<SnapshotErrorEventArgs> onError = (sender, args) => observer.OnError(args.Exception);

				_nextEventHandler = onNext;
				_completedEventHandler = onCompleted;
				_errorEventHandler = onError;

				_snapshotStatusHumbleObject.Next += onNext;
				_snapshotStatusHumbleObject.Completed += onCompleted;
				_snapshotStatusHumbleObject.Error += onError;

				_timer = new Timer(Snapshot, observer, _interval, Timeout.InfiniteTimeSpan);
			}
			catch (Exception exception)
			{
				observer.OnError(exception);
			}

			return this;
		}

		private void Snapshot(object state)
		{
			var observer = state as IObserver<SnapshotStatusResponse>;

			if (observer == null) throw new ArgumentException("state");

			try
			{
				var watch = new Stopwatch();
				watch.Start();

				_snapshotStatusHumbleObject.CheckStatus();

				_timer.Change(TimeSpan.FromMilliseconds(Math.Max(0, _interval.TotalMilliseconds - watch.ElapsedMilliseconds)),
					Timeout.InfiniteTimeSpan);
			}
			catch (Exception exception)
			{
				observer.OnError(exception);
				StopTimer(null, null);
			}
		}

		private void StopTimer(object sender, EventArgs restoreCompletedEventArgs) => _timer.Change(Timeout.Infinite, Timeout.Infinite);

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			_timer?.Dispose();

			if (_snapshotStatusHumbleObject != null)
			{
				_snapshotStatusHumbleObject.Next -= _nextEventHandler;
				_snapshotStatusHumbleObject.Completed -= _completedEventHandler;
				_snapshotStatusHumbleObject.Error -= _errorEventHandler;

				_snapshotStatusHumbleObject.Completed -= StopTimer;
				_snapshotStatusHumbleObject.Error -= StopTimer;
			}

			_disposed = true;
		}

		~SnapshotObservable() => Dispose(false);
	}

	public class SnapshotNextEventArgs : EventArgs
	{
		public SnapshotNextEventArgs(SnapshotStatusResponse snapshotStatusResponse) => SnapshotStatusResponse = snapshotStatusResponse;

		public SnapshotStatusResponse SnapshotStatusResponse { get; }
	}

	public class SnapshotCompletedEventArgs : EventArgs
	{
		public SnapshotCompletedEventArgs(SnapshotStatusResponse snapshotStatusResponse) => SnapshotStatusResponse = snapshotStatusResponse;

		public SnapshotStatusResponse SnapshotStatusResponse { get; private set; }
	}

	public class SnapshotErrorEventArgs : EventArgs
	{
		public SnapshotErrorEventArgs(Exception exception) => Exception = exception;

		public Exception Exception { get; }
	}

	public class SnapshotStatusHumbleObject
	{
		private readonly IElasticClient _elasticClient;
		private readonly ISnapshotRequest _snapshotRequest;
		
		public SnapshotStatusHumbleObject(IElasticClient elasticClient, ISnapshotRequest snapshotRequest)
		{
			elasticClient.ThrowIfNull(nameof(elasticClient));
			snapshotRequest.ThrowIfNull(nameof(snapshotRequest));

			_elasticClient = elasticClient;
			_snapshotRequest = snapshotRequest;
		}

		public event EventHandler<SnapshotCompletedEventArgs> Completed;
		public event EventHandler<SnapshotErrorEventArgs> Error;
		public event EventHandler<SnapshotNextEventArgs> Next;

		public void CheckStatus()
		{
			try
			{
				var snapshotRequest = new SnapshotStatusRequest(_snapshotRequest.RepositoryName, _snapshotRequest.Snapshot)
				{
					RequestConfiguration = new RequestConfiguration()
				};
			
				snapshotRequest.RequestConfiguration.SetRequestMetaData(RequestMetaDataFactory.SnapshotHelperRequestMetaData());

				var snapshotStatusResponse =
					_elasticClient.Snapshot.Status(snapshotRequest);

				if (!snapshotStatusResponse.IsValid)
					throw new ElasticsearchClientException(PipelineFailure.BadResponse, "Failed to get snapshot status.",
						snapshotStatusResponse.ApiCall);

				if (snapshotStatusResponse.Snapshots.All(s => s.ShardsStats.Done == s.ShardsStats.Total))
				{
					OnCompleted(new SnapshotCompletedEventArgs(snapshotStatusResponse));
					return;
				}

				OnNext(new SnapshotNextEventArgs(snapshotStatusResponse));
			}
			catch (Exception exception)
			{
				OnError(new SnapshotErrorEventArgs(exception));
			}
		}

		protected virtual void OnNext(SnapshotNextEventArgs nextEventArgs)
		{
			var handler = Next;
			handler?.Invoke(this, nextEventArgs);
		}

		protected virtual void OnCompleted(SnapshotCompletedEventArgs completedEventArgs)
		{
			var handler = Completed;
			handler?.Invoke(this, completedEventArgs);
		}

		protected virtual void OnError(SnapshotErrorEventArgs errorEventArgs)
		{
			var handler = Error;
			handler?.Invoke(this, errorEventArgs);
		}
	}
}
