using System;
using System.Threading;

namespace Nest
{
	public static class BlockingSubscribeExtensions
	{
		public static BulkAllObserver Wait<T>(this BulkAllObservable<T> observable, TimeSpan maximumRunTime, Action<IBulkAllResponse> onNext)
			where T : class =>
			WaitOnObservable<BulkAllObservable<T>, IBulkAllResponse, BulkAllObserver>(
				observable, maximumRunTime, (e, c) => new BulkAllObserver(onNext, e, c));

		public static ScrollAllObserver<T> Wait<T>(this IObservable<IScrollAllResponse<T>> observable, TimeSpan maximumRunTime, Action<IScrollAllResponse<T>> onNext)
			where T : class =>
			WaitOnObservable<IObservable<IScrollAllResponse<T>>, IScrollAllResponse<T>, ScrollAllObserver<T>>(
				observable, maximumRunTime, (e, c) => new ScrollAllObserver<T>(onNext, e, c));

		public static ReindexObserver Wait(this IObservable<IBulkAllResponse> observable, TimeSpan maximumRunTime, Action<IBulkAllResponse> onNext) =>
			WaitOnObservable<IObservable<IBulkAllResponse>, IBulkAllResponse, ReindexObserver>(
				observable, maximumRunTime, (e, c) => new ReindexObserver(onNext, e, c));

		private static TObserver WaitOnObservable<TObservable, TObserve, TObserver>(
			TObservable observable,
			TimeSpan maximumRunTime,
			Func<Action<Exception>, Action, TObserver> factory
		)
			where TObservable : IObservable<TObserve>
			where TObserver : IObserver<TObserve>
		{
			observable.ThrowIfNull(nameof(observable));
			maximumRunTime.ThrowIfNull(nameof(maximumRunTime));
			Exception ex = null;
			var handle = new ManualResetEvent(false);
			var observer = factory(
				e =>
				{
					ex = e;
					handle.Set();
				},
				() => handle.Set()
			);
			observable.Subscribe(observer);
			handle.WaitOne(maximumRunTime);
			if (ex != null) throw ex;
			return observer;
		}
	}
}