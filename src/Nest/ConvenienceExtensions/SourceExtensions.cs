using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides convenience extension methods that make it easier to get the _source for 
	/// a given document given a string or int id.
	/// </summary>
	public static class SourceExtensions
	{
		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">id as string of the document we want the _source from</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred type name for T</param>
		public static T Source<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.Source<T>(s => s.Id(id).Index(index).Type(type));
		}

		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">id as int of the document we want the _source from</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred type name for T</param>
		public static T Source<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.Source<T>(s => s.Id(id).Index(index).Type(type));
		}

		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">id as string of the document we want the _source from</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred type name for T</param>
		public static Task<T> SourceAsync<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.SourceAsync<T>(s => s.Id(id).Index(index).Type(type));
		}

		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">id as int of the document we want the _source from</param>
		/// <param name="index">Optionally override the inferred index name for T</param>
		/// <param name="type">Optionally override the inferred type name for T</param>
		public static Task<T> SourceAsync<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.SourceAsync<T>(s => s.Id(id).Index(index).Type(type));
		}
	}
}