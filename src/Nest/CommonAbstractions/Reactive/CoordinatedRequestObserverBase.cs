// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;

namespace Nest
{
	internal static class CoordinatedRequestDefaults
	{
		public static int BulkAllBackOffRetriesDefault = 0;
		public static TimeSpan BulkAllBackOffTimeDefault = TimeSpan.FromMinutes(1);
		public static int BulkAllMaxDegreeOfParallelismDefault = 4;
		public static int BulkAllSizeDefault = 1000;
		public static int ReindexBackPressureFactor = 4;
		public static int ReindexScrollSize = 500;
	}

	public abstract class CoordinatedRequestObserverBase<T> : IObserver<T>
	{
		private readonly Action _completed;
		private readonly Action<Exception> _onError;
		private readonly Action<T> _onNext;

		protected CoordinatedRequestObserverBase(Action<T> onNext = null, Action<Exception> onError = null, Action completed = null)
		{
			_onNext = onNext;
			_onError = onError;
			_completed = completed;
		}

		public void OnCompleted() => _completed?.Invoke();

		public void OnError(Exception error)
		{
			// This normalizes task cancellation exceptions for observables
			// If a task cancellation happens in the client it bubbles out as a UnexpectedTransportException
			// where as inside our IObservable implementation we .ThrowIfCancellationRequested() directly.
			if (error is UnexpectedTransportException es && es.InnerException != null && es.InnerException is OperationCanceledException c)
				_onError?.Invoke(c);
			else _onError?.Invoke(error);
		}

		public void OnNext(T value) => _onNext?.Invoke(value);
	}
}
