using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Use the /{index}/{type}/{id} to get the document and its metadata
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		public static GetResponse<TDocument> Get<TDocument>(this IElasticClient client,DocumentPath<TDocument> document, Func<GetDescriptor<TDocument>, IGetRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		public static GetResponse<TDocument> Get<TDocument>(this IElasticClient client,IGetRequest request) where TDocument : class;

		/// <inheritdoc />
		public static Task<GetResponse<TDocument>> GetAsync<TDocument>(this IElasticClient client,
			DocumentPath<TDocument> document,
			Func<GetDescriptor<TDocument>, IGetRequest> selector = null,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		public static Task<GetResponse<TDocument>> GetAsync<TDocument>(this IElasticClient client,IGetRequest request, CancellationToken ct = default) where TDocument : class;
	}

}
