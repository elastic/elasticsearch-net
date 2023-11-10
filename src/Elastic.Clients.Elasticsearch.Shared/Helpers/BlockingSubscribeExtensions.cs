// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if !ELASTICSEARCH_SERVERLESS

using System;
using System.Threading;
namespace Elastic.Clients.Elasticsearch;

public static class BlockingSubscribeExtensions
{
	public static BulkAllObserver Wait<T>(this BulkAllObservable<T> observable, TimeSpan maximumRunTime, Action<BulkAllResponse> onNext)
		where T : class =>
		WaitOnObservable<BulkAllObservable<T>, BulkAllResponse, BulkAllObserver>(
			observable, maximumRunTime, (e, c) => new BulkAllObserver(onNext, e, c));

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
		if (ex != null)
			throw ex;

		return observer;
	}
}

#endif
