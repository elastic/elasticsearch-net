using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document,
		/// without any additional content around it.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="document">The document path</param>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		public static TDocument Source<TDocument>(this IElasticClient client,DocumentPath<TDocument> document, Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		public static TDocument Source<TDocument>(this IElasticClient client,ISourceRequest request) where TDocument : class;

		/// <inheritdoc />
		public static Task<TDocument> SourceAsync<TDocument>(this IElasticClient client,
			DocumentPath<TDocument> document,
			Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		public static Task<TDocument> SourceAsync<TDocument>(this IElasticClient client,ISourceRequest request, CancellationToken ct = default) where TDocument : class;
	}

}
