using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	internal class GetEnumerator<TSource> : IEnumerator<TSource>, IObserver<TSource>
	{
		private readonly ConcurrentQueue<TSource> _queue;
		private TSource _current;
		private Exception _error;
		private bool _disposed;

		private readonly SemaphoreSlim _gate;
		private IDisposable _subscription;

		public GetEnumerator()
		{
			_queue = new ConcurrentQueue<TSource>();
			_gate = new SemaphoreSlim(0);
		}

		private IEnumerator<TSource> Run(IObservable<TSource> source)
		{
			//
			// [OK] Use of unsafe Subscribe: non-pretentious exact mirror with the dual GetEnumerator method.
			//
			_subscription = source.Subscribe/*Unsafe*/(this);
			return this;
		}
		public IEnumerable<TSource> ToEnumerable(IObservable<TSource> source) =>
			new AnonymousEnumerable<TSource>(() => this.Run(source));

		public virtual void OnNext(TSource value)
		{
			_queue.Enqueue(value);
			_gate.Release();
		}

		public void OnError(Exception error)
		{
			_error = error;
			_subscription.Dispose();
			_gate.Release();
		}

		public void OnCompleted()
		{
			_subscription.Dispose();
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

		public TSource Current => _current;

		object IEnumerator.Current => _current;

		public void Dispose()
		{
			_subscription.Dispose();

			_disposed = true;
			_gate.Release();
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal sealed class AnonymousEnumerable<T> : IEnumerable<T>
		{
			private readonly Func<IEnumerator<T>> _getEnumerator;

			public AnonymousEnumerable(Func<IEnumerator<T>> getEnumerator)
			{
				this._getEnumerator = getEnumerator;
			}

			public IEnumerator<T> GetEnumerator()
			{
				return _getEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}
	}
}