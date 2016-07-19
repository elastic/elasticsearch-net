using System;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method you can pass an IEnumerable that we will be partionened and send as multiple bulks in parallel
		/// </summary>
		/// <param name="selector">the descriptor to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IBulkAllResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		BulkAllObservable<T> BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
			)
			where T : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
		/// </summary>
		/// <param name="request">a request object to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IBulkAllResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
	}

	public partial class ElasticClient
	{
		///<inheritdoc />
		public BulkAllObservable<T>  BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
			)
			where T : class =>
			this.BulkAll<T>(selector.InvokeOrDefault(new BulkAllDescriptor<T>(documents)), cancellationToken);

		///<inheritdoc />
		public BulkAllObservable<T>  BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			new BulkAllObservable<T>(this, ConnectionSettings, request, cancellationToken);
	}
}
