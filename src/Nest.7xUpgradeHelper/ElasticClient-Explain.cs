using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The explain api computes a score explanation for a query and a specific document.
		/// This can give useful feedback whether a document matches or didn’t match a specific query.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/search-explain.html</a>
		/// </summary>
		public static ExplainResponse<TDocument> Explain<TDocument>(this IElasticClient client,DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector
		)
			where TDocument : class;

		/// <inheritdoc />
		public static ExplainResponse<TDocument> Explain<TDocument>(this IElasticClient client,IExplainRequest<TDocument> request)
			where TDocument : class;

		/// <inheritdoc />
		public static Task<ExplainResponse<TDocument>> ExplainAsync<TDocument>(this IElasticClient client,DocumentPath<TDocument> document,
			Func<ExplainDescriptor<TDocument>, IExplainRequest<TDocument>> selector, CancellationToken ct = default
		)
			where TDocument : class;

		/// <inheritdoc />
		public static Task<ExplainResponse<TDocument>> ExplainAsync<TDocument>(this IElasticClient client,IExplainRequest<TDocument> request,
			CancellationToken ct = default
		)
			where TDocument : class;
	}

}
