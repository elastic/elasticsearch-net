using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides GetMany extensions that make it easier to get many documents given a list of ids
	/// </summary>
	public static class IndexManyExtensions
	{
		//TODO nullable IndexName and IndexType?

		/// <summary>
		/// Shortcut into the Bulk call that indexes the specified objects
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="client"></param>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="index">Override the inferred indexname for T</param>
		/// <param name="type">Override the inferred typename for T</param>
		public static IBulkResponse IndexMany<T>(this IElasticClient client, IEnumerable<T> @objects, string index = null, string type = null) where T : class
		{
			var bulkRequest = CreateIndexBulkRequest(objects, index, type);
			return client.Bulk(bulkRequest);
		}
		

		/// <summary>
		/// Shortcut into the Bulk call that indexes the specified objects
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="client"></param>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="objects">List of objects to index, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="index">Override the inferred indexname for T</param>
		/// <param name="type">Override the inferred typename for T</param>
		public static Task<IBulkResponse> IndexManyAsync<T>(this IElasticClient client, IEnumerable<T> objects, string index = null, string type = null)
			where T : class
		{
			var bulkRequest = CreateIndexBulkRequest(objects, index, type);
			return client.BulkAsync(bulkRequest);
		}
		
		private static BulkRequest CreateIndexBulkRequest<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			@objects.ThrowIfEmpty(nameof(objects));
			var bulkRequest = new BulkRequest(index, type);
			var indexOps = @objects
				.Select(o => new BulkIndexOperation<T>(o))
				.Cast<IBulkOperation>()
				.ToList();
			bulkRequest.Operations = indexOps;
			return bulkRequest;
		}
	}
}