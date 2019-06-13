using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html</a>
		/// </summary>
		/// <typeparam name="T">
		/// The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query
		/// </typeparam>
		/// <param name="selector">An optional descriptor to further describe the delete by query operation</param>
		public static DeleteByQueryResponse DeleteByQuery<T>(this IElasticClient client,Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		public static DeleteByQueryResponse DeleteByQuery(this IElasticClient client,IDeleteByQueryRequest request);

		/// <inheritdoc />
		public static Task<DeleteByQueryResponse> DeleteByQueryAsync<T>(this IElasticClient client,Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<DeleteByQueryResponse> DeleteByQueryAsync(this IElasticClient client,IDeleteByQueryRequest request,
			CancellationToken ct = default
		);
	}

}
