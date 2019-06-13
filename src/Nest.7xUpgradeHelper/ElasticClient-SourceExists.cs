using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using ExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Check if a document's source exists without returning its contents
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe what document we are looking for</param>
		public static ExistsResponse SourceExists<TDocument>(this IElasticClient client,DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null)
			where TDocument : class;

		/// <inheritdoc />
		public static ExistsResponse SourceExists(this IElasticClient client,ISourceExistsRequest request);

		/// <inheritdoc />
		public static Task<ExistsResponse> SourceExistsAsync<TDocument>(this IElasticClient client,DocumentPath<TDocument> document, Func<SourceExistsDescriptor<TDocument>, ISourceExistsRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class;

		/// <inheritdoc />
		public static Task<ExistsResponse> SourceExistsAsync(this IElasticClient client,ISourceExistsRequest request, CancellationToken ct = default);
	}

}
