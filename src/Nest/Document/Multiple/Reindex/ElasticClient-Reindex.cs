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
		IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			Func<TSource, TTarget> mapper,
			Func<ReindexDescriptor<TSource,TTarget>, IReindexRequest<TSource,TTarget>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class
			where TTarget : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using ScrollAll and BulkAll.
		/// </summary>
		/// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		IObservable<IBulkAllResponse> Reindex<TSource>(
			Func<ReindexDescriptor<TSource, TSource>, IReindexRequest<TSource, TSource>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using ScrollAll and BulkAll.
		/// </summary>
		/// <param name="request">a request object to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			IReindexRequest<TSource,TTarget> request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class
			where TTarget : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using ScrollAll and BulkAll.
		/// </summary>
		/// <param name="request">a request object to describe the reindex operation</param>
		/// <returns>An IObservable&lt;IReindexResponse&lt;T&gt;$gt; you can subscribe to to listen to the progress of the reindex process</returns>
		IObservable<IBulkAllResponse> Reindex<TSource>(
			IReindexRequest<TSource> request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class;

		/// <summary>
		/// Simplified form for reindex which will cover 80% of its usecases. Allows you to index all documents of type T from <paramref name="fromIndex" /> to <paramref name="toIndex" />
		/// optionally limitting the documents found in <paramref name="fromIndex" /> by using <paramref name="selector"/>.
		/// </summary>
		/// <param name="fromIndex">The source index, from which all types will be returned</param>
		/// <param name="toIndex">The target index, if it does not exist already will be created using the same settings of <paramref name="fromIndex"/></param>
		/// <param name="selector">an optional query limitting the documents found in <paramref name="fromIndex"/></param>
		IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<TSource, TTarget> mapper,
			Func<QueryContainerDescriptor<TSource>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class
			where TTarget : class;

		/// <summary>
		/// Simplified form for reindex which will cover 80% of its usecases. Allows you to index all documents of type T from <paramref name="fromIndex" /> to <paramref name="toIndex" />
		/// optionally limitting the documents found in <paramref name="fromIndex" /> by using <paramref name="selector"/>.
		/// </summary>
		/// <param name="fromIndex">The source index, from which all types will be returned</param>
		/// <param name="toIndex">The target index, if it does not exist already will be created using the same settings of <paramref name="fromIndex"/></param>
		/// <param name="selector">an optional query limitting the documents found in <paramref name="fromIndex"/></param>
		IObservable<IBulkAllResponse> Reindex<TSource>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<QueryContainerDescriptor<TSource>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource>(
			Func<ReindexDescriptor<TSource,TSource>, IReindexRequest<TSource,TSource>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class =>
			this.Reindex<TSource,TSource>(selector.InvokeOrDefault(new ReindexDescriptor<TSource,TSource>(s=>s)));

		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			Func<TSource, TTarget> mapper,
			Func<ReindexDescriptor<TSource,TTarget>, IReindexRequest<TSource,TTarget>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TTarget : class
			where TSource : class =>
			this.Reindex<TSource,TTarget>(selector.InvokeOrDefault(new ReindexDescriptor<TSource,TTarget>(mapper)));

		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource>(
			IReindexRequest<TSource> request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class =>
			this.Reindex<TSource, TSource>(request, cancellationToken);


		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			IReindexRequest<TSource,TTarget> request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TTarget : class
			where TSource : class
		{
			if (request.ScrollAll == null)
				throw new ArgumentException(
					"Reindex must have ScrollAll property set so we know how to get the source of the reindex operation");
			if (request.BulkAll == null)
				throw new ArgumentException(
					"Reindex must have BulkAll property set so we know how to get the target of the reindex operation");
			return new ReindexObservable<TSource,TTarget>(this, ConnectionSettings, request, cancellationToken);
		}

		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource,TTarget>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<TSource, TTarget> mapper,
			Func<QueryContainerDescriptor<TSource>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TTarget : class
			where TSource : class =>
			this.Reindex<TSource,TTarget>(
				mapper,
				SimplifiedReindexer<TSource, TTarget>(fromIndex, toIndex, selector)
			, cancellationToken);

		/// <inheritdoc />
		public IObservable<IBulkAllResponse> Reindex<TSource>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<QueryContainerDescriptor<TSource>, QueryContainer> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where TSource : class =>
			this.Reindex<TSource,TSource>(
				s=>s,
				SimplifiedReindexer<TSource, TSource>(fromIndex, toIndex, selector)
			, cancellationToken);

		private static Func<ReindexDescriptor<TSource,TTarget>, IReindexRequest<TSource,TTarget>> SimplifiedReindexer<TSource, TTarget>(
			IndexName fromIndex,
			IndexName toIndex,
			Func<QueryContainerDescriptor<TSource>, QueryContainer> selector
		)
			where TTarget : class
			where TSource : class
		{
			return r => r
				.ScrollAll("1m", -1, search => search
					.Search(ss => ss
						.Size(CoordinatedRequestDefaults.ReindexScrollSize)
						.Index(fromIndex)
						.Query(selector)
					)
				)
				.BulkAll(b => b.Index(toIndex));
		}


	}
}