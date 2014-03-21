using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Implements Get() extensions that make it easier to get a document given an id
	/// </summary>
	public static class GetExtensions
	{
		/// <summary>
		/// The get API allows to get a typed JSON document from the index based on its id.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The string id of the document we want the fetch</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred typename for T</param>
		public static IGetResponse<T> Get<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.Get<T>(s => s.Id(id).Index(index).Type(type));
		}
		
		/// <summary>
		/// The get API allows to get a typed JSON document from the index based on its id.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The int id of the document we want the fetch</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred typename for T</param>
		public static IGetResponse<T> Get<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.Get<T>(s => s.Id(id).Index(index).Type(type));
		}

		/// <summary>
		/// The get API allows to get a typed JSON document from the index based on its id.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The string id of the document we want the fetch</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred typename for T</param>
		public static Task<IGetResponse<T>> GetAsync<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.GetAsync<T>(s => s.Id(id).Index(index).Type(type));
		}

		/// <summary>
		/// The get API allows to get a typed JSON document from the index based on its id.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The int id of the document we want the fetch</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred typename for T</param>
		public static Task<IGetResponse<T>> GetAsync<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.GetAsync<T>(s => s.Id(id).Index(index).Type(type));
		}
	}
}