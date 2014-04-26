using System;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Implements DeleteMapping() extensions that allow for easier strongly typed deletes of types.
	/// </summary>
	public static class DeleteMappingExtensions
	{
		/// <summary>
		/// Allow to delete a mapping (type) along with its data. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-mapping.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="selector">An optional descriptor to further describe the delete mapping operation</param>
		public static IIndicesResponse DeleteMapping<T>(
			this IElasticClient client,
			Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteMapping(s => selector(s.Index<T>().Type<T>()));
		}

		/// <summary>
		/// Allow to delete a mapping (type) along with its data. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-mapping.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="selector">An optional descriptor to further describe the delete mapping operation</param>
		public static Task<IIndicesResponse> DeleteMappingAsync<T>(
			this IElasticClient client,
			Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteMappingAsync(s => selector(s.Index<T>().Type<T>()));
		}
	}
}