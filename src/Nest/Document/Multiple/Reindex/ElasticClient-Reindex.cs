using System;
using System.Collections.Generic;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
		/// </summary>
		/// <returns>An IObservable you can subscribe to to listen to the progress of the reindexation process</returns>
		IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, ReindexDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest reindexRequest) where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, ReindexDescriptor<T>> selector = null)
			where T : class => 
			this.Reindex<T>(selector.InvokeOrDefault(new ReindexDescriptor<T>(from, to)));

		/// <inheritdoc/>
		public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest reindexRequest)
			where T : class => 
			new ReindexObservable<T>(this, ConnectionSettings, reindexRequest);
	}
}