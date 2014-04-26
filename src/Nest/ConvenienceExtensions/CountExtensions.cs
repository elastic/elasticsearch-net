using System;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>Implements a convenience extension method for count that defaults
	/// to counting over all indices and types.
	/// </summary>
	public static class CountExtensions
	{

		/// <summary>
		/// The count API allows to easily execute a query and get the number of matches for that query. 
		/// It can be executed across one or more indices and across one or more types. 
		/// <para>This overload returns a dynamic response and defaults to all types and indices</para>
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="countSelector">An optional descriptor to describe the count operation</param>
		public static ICountResponse Count(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.Count<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}

		/// <summary>
		/// The count API allows to easily execute a query and get the number of matches for that query. 
		/// It can be executed across one or more indices and across one or more types. 
		/// <para>This overload returns a dynamic response and defaults to all types and indices</para>
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="countSelector">An optional descriptor to describe the count operation</param>
		public static Task<ICountResponse> CountAsync(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.CountAsync<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}
	}
}