using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The search API allows to execute a search query and get back search hits that match the query.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-search.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		public static SearchResponse<T> Search<T>(this IElasticClient client,Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class;

		/// <inheritdoc />
		public static SearchResponse<T> Search<T>(this IElasticClient client,ISearchRequest request) where T : class;

		/// <inheritdoc />
		SearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		SearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		public static Task<SearchResponse<T>> SearchAsync<T>(this IElasticClient client,Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<SearchResponse<T>> SearchAsync<T>(this IElasticClient client,ISearchRequest request, CancellationToken ct = default)
			where T : class;

		/// <inheritdoc />
		Task<SearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<SearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request,
			CancellationToken ct = default
		)
			where T : class
			where TResult : class;
	}


}
