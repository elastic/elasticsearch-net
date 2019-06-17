using System;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to Elasticsearch as bulks concurrently
		/// </summary>
		/// <param name="documents">The lazy stream of documents</param>
		BulkAllObservable<T> BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default
		)
			where T : class;

		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to Elasticsearch as bulks concurrently
		/// </summary>
		BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default) where T : class;
	}

	public partial class ElasticClient
	{
		///<inheritdoc />
		public BulkAllObservable<T> BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default
		)
			where T : class =>
			BulkAll(selector.InvokeOrDefault(new BulkAllDescriptor<T>(documents)), cancellationToken);

		///<inheritdoc />
		public BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default)
			where T : class =>
			new BulkAllObservable<T>(this, request, cancellationToken);
	}
}
