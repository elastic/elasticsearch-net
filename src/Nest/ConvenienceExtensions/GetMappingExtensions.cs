using System;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provide GetMapping() extensions that make it easier to get a mapping given a CLR type
	/// </summary>
	public static class GetMappingExtensions
	{
		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="selector">An optional descriptor that describes the parameters for the get mapping operation</param>
		public static IGetMappingResponse GetMapping<T>(
			this IElasticClient client,
			Func<GetMappingDescriptor, GetMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.GetMapping(s => selector(s.Index<T>().Type<T>()));
		}

		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="selector">An optional descriptor that describes the parameters for the get mapping operation</param>
		public static Task<IGetMappingResponse> GetMappingAsync<T>(
			this IElasticClient client,
			Func<GetMappingDescriptor, GetMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.GetMappingAsync(s => selector(s.Index<T>().Type<T>()));
		}
	}
}