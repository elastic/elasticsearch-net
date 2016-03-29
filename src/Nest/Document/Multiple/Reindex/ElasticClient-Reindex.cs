using System;

namespace Nest
{
	public partial interface IElasticClient
	{
        /// <summary>
        /// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
        /// </summary>
        /// <param name="from">the index that documents will be reindexed from</param>
        /// <param name="to">the index that documents will be reindexed to</param>
        /// <param name="selector">the descriptor to describe the reindex operation</param>
        /// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
        IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector = null)
			where T : class;

        /// <summary>
        /// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
        /// </summary>
        /// <param name="request">a request object to describe the reindex operation</param>
        /// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
        IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request) where T : class;
	}

	public partial class ElasticClient
	{
        /// <summary>
        /// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
        /// </summary>
        /// <param name="from">the index that documents will be reindexed from</param>
        /// <param name="to">the index that documents will be reindexed to</param>
        /// <param name="selector">the descriptor to describe the reindex operation</param>
        /// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
        public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector = null)
			where T : class =>
			this.Reindex<T>(selector.InvokeOrDefault(new ReindexDescriptor<T>()));

        /// <summary>
        /// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
        /// </summary>
        /// <param name="request">a request object to describe the reindex operation</param>
        /// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
        public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request)
			where T : class =>
			new ReindexObservable<T>(this, ConnectionSettings, request);
	}
}
