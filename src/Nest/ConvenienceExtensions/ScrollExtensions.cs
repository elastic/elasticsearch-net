using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides extension methods to provide a cleaner scoll API given a scollTime and scrollId
	/// </summary>
	public static class ScrollExtensions
	{
		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. 
		/// <para>The scroll parameter is a time value parameter (for example: scroll=5m), 
		/// indicating for how long the nodes that participate in the search will maintain relevant resources in
		/// order to continue and support it.</para><para> 
		/// This is very similar in its idea to opening a cursor against a database.</para>
		/// </summary>
		/// <typeparam name="T">The type that represents the result hits</typeparam>
		/// <param name="client"></param>
		/// <param name="scrollTime">The time the server should wait for the scroll before closing the scan operation</param>
		/// <param name="scrollId">The scroll id to continue the scroll operation</param>
		public static ISearchResponse<T> Scroll<T>(this IElasticClient client, string scrollTime, string scrollId) where T : class
		{
			return client.Scroll<T>(s => s.Scroll(scrollTime).ScrollId(scrollId));
		}

		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. 
		/// <para>The scroll parameter is a time value parameter (for example: scroll=5m), 
		/// indicating for how long the nodes that participate in the search will maintain relevant resources in
		/// order to continue and support it.</para><para> 
		/// This is very similar in its idea to opening a cursor against a database.</para>
		/// </summary>
		/// <typeparam name="T">The type that represents the result hits</typeparam>
		/// <param name="client"></param>
		/// <param name="scrollTime">The time the server should wait for the scroll before closing the scan operation</param>
		/// <param name="scrollId">The scroll id to continue the scroll operation</param>
		public static Task<ISearchResponse<T>> ScrollAsync<T>(this IElasticClient client, string scrollTime, string scrollId) where T : class
		{
			return client.ScrollAsync<T>(s => s.Scroll(scrollTime).ScrollId(scrollId));
		}
		
		/// <summary>
		/// Deletes a registered scroll request on the cluster 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="selector">Specify the scroll id as well as request specific configuration</param>
		public static IEmptyResponse ClearScroll(this IElasticClient client, string scrollId) 
		{
			return client.ClearScroll(s => s.ScrollId(scrollId));
		}
		
		/// <summary>
		/// Deletes a registered scroll request on the cluster 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="selector">Specify the scroll id as well as request specific configuration</param>
		public static Task<IEmptyResponse> ClearScrollAsync(this IElasticClient client, string scrollId) 
		{
			return client.ClearScroll(s => s.ScrollId(scrollId));
		}
	}
}