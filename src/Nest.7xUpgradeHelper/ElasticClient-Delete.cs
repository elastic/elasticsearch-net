using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The delete API allows to delete a typed JSON document from a specific index based on its id.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe the delete operation, i.e type/index/id</param>
		public static DeleteResponse Delete<TDocument>(this IElasticClient client,DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		public static DeleteResponse Delete(this IElasticClient client,IDeleteRequest request);

		/// <inheritdoc />
		public static Task<DeleteResponse> DeleteAsync<TDocument>(this IElasticClient client,
			DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		public static Task<DeleteResponse> DeleteAsync(this IElasticClient client,IDeleteRequest request, CancellationToken ct = default);
	}

}
