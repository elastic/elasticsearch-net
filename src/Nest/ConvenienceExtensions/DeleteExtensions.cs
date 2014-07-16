using System;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Implements extensions to Delete that allow for easier by id deletes.
	/// </summary>
	public static class DeleteExtensions
	{

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index as string</param>
		/// <param name="type">The type name of the document you wish to delete</param>
		/// <param name="id">The id as string of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static IDeleteResponse Delete(this IElasticClient client, string index, string type, string id, 
			Func<DeleteDescriptor<object>, DeleteDescriptor<object>> selector = null) 
		{
			selector = selector ?? (s => s);
			return client.Delete<object>(s => selector(s.Index(index).Type(type).Id(id)));
		}
		
		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The id as int of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static IDeleteResponse Delete<T>(this IElasticClient client, long id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.Delete<T>(s => selector(s.Id(id)));
		}

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The id as string of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static IDeleteResponse Delete<T>(this IElasticClient client, string id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.Delete<T>(s => selector(s.Id(id)));
		}

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The id as int of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static Task<IDeleteResponse> DeleteAsync<T>(this IElasticClient client, long id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteAsync<T>(s => selector(s.Id(id)));
		}

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index as string</param>
		/// <param name="type">The type name of the document you wish to delete</param>
		/// <param name="id">The id as string of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static Task<IDeleteResponse> DeleteAsync(this IElasticClient client, 
			string index, string type, string id, 
			Func<DeleteDescriptor<object>, DeleteDescriptor<object>> selector = null) 
		{
			selector = selector ?? (s => s);
			return client.DeleteAsync<object>(s => selector(s.Index(index).Type(type).Id(id)));
		}
		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="id">The id as string of the document you want to delete</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static Task<IDeleteResponse> DeleteAsync<T>(this IElasticClient client, string id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteAsync<T>(s => selector(s.Id(id)));
		}
		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="obj">The object used to infer the id</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static IDeleteResponse Delete<T>(this IElasticClient client, T obj, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			obj.ThrowIfNull("obj");
			var id = client.Infer.Id(obj);
			selector = selector ?? (s => s);
			return client.Delete<T>(s => selector(s.Id(id)));
		}

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="obj">The object used to infer the id</param>
		/// <param name="selector">An optional descriptor to further describe the delete operation</param>
		public static Task<IDeleteResponse> DeleteAsync<T>(this IElasticClient client, T obj, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			obj.ThrowIfNull("obj");
			var id = client.Infer.Id(obj);
			selector = selector ?? (s => s);
			return client.DeleteAsync<T>(s => selector(s.Id(id)));
		}
	}
}