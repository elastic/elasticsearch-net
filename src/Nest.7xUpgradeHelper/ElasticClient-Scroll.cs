using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter.
		/// <para>
		/// The scroll parameter is a time value parameter (for example: scroll=5m),
		/// indicating for how long the nodes that participate in the search will maintain relevant resources in
		/// order to continue and support it.
		/// </para>
		/// <para>
		/// This is very similar in its idea to opening a cursor against a database.
		/// </para>
		/// <para> </para>
		/// <para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html</para>
		/// </summary>
		/// <typeparam name="T">The type that represents the result hits</typeparam>
		/// <param name="request">A descriptor that describes the scroll operation</param>
		/// <returns>A query response holding <typeparamref name="T" /> hits as well as the ScrollId for the next scroll operation</returns>
		public static SearchResponse<T> Scroll<T>(this IElasticClient client,IScrollRequest request) where T : class;

		/// <inheritdoc />
		public static SearchResponse<T> Scroll<T>(this IElasticClient client,Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		public static Task<SearchResponse<T>> ScrollAsync<T>(this IElasticClient client,IScrollRequest request, CancellationToken ct = default)
			where T : class;

		/// <inheritdoc />
		public static Task<SearchResponse<T>> ScrollAsync<T>(this IElasticClient client,Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;
	}

}
