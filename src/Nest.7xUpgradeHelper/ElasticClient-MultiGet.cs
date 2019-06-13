using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <param name="selector">A descriptor describing which documents should be fetched</param>
		public static MultiGetResponse MultiGet(this IElasticClient client,Func<MultiGetDescriptor, IMultiGetRequest> selector = null);

		/// <inheritdoc />
		public static MultiGetResponse MultiGet(this IElasticClient client,IMultiGetRequest request);

		/// <inheritdoc />
		public static Task<MultiGetResponse> MultiGetAsync(this IElasticClient client,Func<MultiGetDescriptor, IMultiGetRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<MultiGetResponse> MultiGetAsync(this IElasticClient client,IMultiGetRequest request, CancellationToken ct = default);
	}

}
