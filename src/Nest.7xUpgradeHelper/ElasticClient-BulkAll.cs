using System;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to elasticsearch as bulks concurrently
		/// </summary>
		/// <param name="documents">The lazy stream of documents</param>
		public static BulkAllObservable<T> BulkAll<T>(this IElasticClient client,
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to elasticsearch as bulks concurrently
		/// </summary>
		public static BulkAllObservable<T> BulkAll<T>(this IElasticClient client,IBulkAllRequest<T> request, CancellationToken ct = default) where T : class;
	}

}
