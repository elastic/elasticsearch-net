using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The bulk API makes it possible to perform many index/delete operations in a single API call.
		/// This can greatly increase the indexing speed.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="request">A descriptor the describe the index/create/delete operation for this bulk operation</param>
		public static BulkResponse Bulk(this IElasticClient client,IBulkRequest request);

		/// <inheritdoc />
		public static BulkResponse Bulk(this IElasticClient client,Func<BulkDescriptor, IBulkRequest> selector);

		/// <inheritdoc />
		public static Task<BulkResponse> BulkAsync(this IElasticClient client,IBulkRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		public static Task<BulkResponse> BulkAsync(this IElasticClient client,Func<BulkDescriptor, IBulkRequest> selector, CancellationToken ct = default);
	}
}
