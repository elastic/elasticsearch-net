using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html</a>
		/// </summary>
		/// <typeparam name="T">The type of the document</typeparam>
		/// <param name="selector">A descriptor for the terms vector operation</param>
		public static TermVectorsResponse TermVectors<T>(this IElasticClient client,Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)
			where T : class;

		/// <inheritdoc />
		public static TermVectorsResponse TermVectors<T>(this IElasticClient client,ITermVectorsRequest<T> request)
			where T : class;

		/// <inheritdoc />
		public static Task<TermVectorsResponse> TermVectorsAsync<T>(this IElasticClient client,
			Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<TermVectorsResponse> TermVectorsAsync<T>(this IElasticClient client,ITermVectorsRequest<T> request,
			CancellationToken ct = default
		)
			where T : class;
	}

}
