// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	internal class GetEnumerator<TSource> : IEnumerator<TSource>, IObserver<TSource>
	{
		private readonly SemaphoreSlim _gate;
		private readonly ConcurrentQueue<TSource> _queue;
		private TSource _current;
		private bool _disposed;
		private Exception _error;
		private IDisposable _subscription;

		public GetEnumerator()
		{
			_queue = new ConcurrentQueue<TSource>();
			_gate = new SemaphoreSlim(0);
		}

		public TSource Current => _current;

		object IEnumerator.Current => _current;

		public void Dispose()
		{
			_subscription.Dispose();

			_disposed = true;
			_gate.Release();
		}

		public bool MoveNext()
		{
			_gate.Wait();

			if (_disposed)
				throw new ObjectDisposedException("");

			if (_queue.TryDequeue(out _current))
				return true;

			if (_error != null) throw _error;

			_gate.Release(); // In the (rare) case the user calls MoveNext again we shouldn't block!
			return false;
		}

		public void Reset() => throw new NotSupportedException();

		public void OnCompleted()
		{
			_subscription.Dispose();
			_gate.Release();
		}

		public void OnError(Exception error)
		{
			_error = error;
			_subscription.Dispose();
			_gate.Release();
		}

		public virtual void OnNext(TSource value)
		{
			_queue.Enqueue(value);
			_gate.Release();
		}

		private IEnumerator<TSource> Run(IObservable<TSource> source)
		{
			//
			// [OK] Use of unsafe Subscribe: non-pretentious exact mirror with the dual GetEnumerator method.
			//
			_subscription = source.Subscribe /*Unsafe*/(this);
			return this;
		}

		public IEnumerable<TSource> ToEnumerable(IObservable<TSource> source) =>
			new AnonymousEnumerable<TSource>(() => Run(source));

		internal sealed class AnonymousEnumerable<T> : IEnumerable<T>
		{
			private readonly Func<IEnumerator<T>> _getEnumerator;

			public AnonymousEnumerable(Func<IEnumerator<T>> getEnumerator) => _getEnumerator = getEnumerator;

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

			public IEnumerator<T> GetEnumerator() => _getEnumerator();
		}
	}
}
