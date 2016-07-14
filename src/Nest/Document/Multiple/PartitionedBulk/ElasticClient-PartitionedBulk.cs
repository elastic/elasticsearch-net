using System;
using System.Collections.Generic;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method you can pass an IEnumerable that we will be partionened and send as multiple bulks in parallel
		/// </summary>
		/// <param name="selector">the descriptor to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IPartitionedBulkResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		BulkObservable<T> PartitionedBulk<T>(
			IEnumerable<T> documents,
			Func<PartitionedBulkDescriptor<T>, IPartitionedBulkRequest<T>> selector = null)
			where T : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
		/// </summary>
		/// <param name="request">a request object to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IPartitionedBulkResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		BulkObservable<T> PartitionedBulk<T>(IPartitionedBulkRequest<T> request) where T : class;
	}

	public partial class ElasticClient
	{
		public BulkObservable<T>  PartitionedBulk<T>(
			IEnumerable<T> documents,
			Func<PartitionedBulkDescriptor<T>, IPartitionedBulkRequest<T>> selector = null)
			where T : class =>
			this.PartitionedBulk<T>(selector.InvokeOrDefault(new PartitionedBulkDescriptor<T>(documents)));

		public BulkObservable<T>  PartitionedBulk<T>(IPartitionedBulkRequest<T> request)
			where T : class =>
			new BulkObservable<T>(this, ConnectionSettings, request);
	}
}
