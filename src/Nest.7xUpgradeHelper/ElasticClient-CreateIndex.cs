using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices,
		/// including executing operations across several indices.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the create index operation</param>
		public static CreateIndexResponse CreateIndex(this IElasticClient client,IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc />
		public static CreateIndexResponse CreateIndex(this IElasticClient client,ICreateIndexRequest request);

		/// <inheritdoc />
		public static Task<CreateIndexResponse> CreateIndexAsync(this IElasticClient client,
			IndexName index,
			Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CreateIndexResponse> CreateIndexAsync(this IElasticClient client,ICreateIndexRequest request, CancellationToken ct = default);
	}

}
