using System;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method that allows you to reindex from one index into another using ScrollAll and BulkAll.
		/// </summary>
		/// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		IObservable<IBulkAllResponse> Reindex<T>(
			Func<ReindexDescriptor<T>, IReindexRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <summary>
		/// Simplified form for reindex which will cover 80% of its usecases. Allows you to index all documents of type T from <paramref name="fromIndex" /> to <paramref name="toIndex" />
		/// optionally limitting the documents found in <paramref name="fromIndex" /> by using <paramref name="selector"/>.
		/// </summary>
		/// <param name="fromIndex">The source index, from which all types will be returned</param>
		/// <param name="toIndex">The target index, if it does not exist already will be created using the same settings of <paramref name="fromIndex"/></param>
		/// <param name="selector">an optional query limitting the documents found in <paramref name="fromIndex"/></param>
		IObservable<IBulkAllResponse> Reindex<T>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<QueryContainerDescriptor<T>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using ScrollAll and BulkAll.
		/// </summary>
		/// <param name="request">a request object to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		IObservable<IBulkAllResponse> Reindex<T>(
			IReindexRequest<T> request,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<T>(
			Func<ReindexDescriptor<T>, IReindexRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			this.Reindex<T>(selector.InvokeOrDefault(new ReindexDescriptor<T>()));

		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<T>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<QueryContainerDescriptor<T>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			this.Reindex<T>(r => r
					.ScrollAll("1m", -1, search => search.Search(ss => ss.Index(fromIndex).Query(selector)))
					.BulkAll(b => b.Index(toIndex))
			, cancellationToken);


		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<T>(
			IReindexRequest<T> request,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class
		{
			if (request.ScrollAll == null)
				throw new ArgumentException(
					"Reindex must have ScrollAll property set so we know how to get the source of the reindex operation");
			if (request.BulkAll == null)
				throw new ArgumentException(
					"Reindex must have BulkAll property set so we know how to get the target of the reindex operation");
			return new ReindexObservable<T>(this, ConnectionSettings, request, cancellationToken);
		}
	}
}