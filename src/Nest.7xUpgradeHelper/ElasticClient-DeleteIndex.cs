using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		public static DeleteIndexResponse DeleteIndex(this IElasticClient client,Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null);

		/// <inheritdoc />
		public static DeleteIndexResponse DeleteIndex(this IElasticClient client,IDeleteIndexRequest request);

		/// <inheritdoc />
		public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client,
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client,IDeleteIndexRequest request, CancellationToken ct = default);
	}

}
