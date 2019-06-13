using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Used to check if the index (indices) exists or not.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-exists.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the index exist operation</param>
		public static ExistsResponse IndexExists(this IElasticClient client,Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc />
		public static ExistsResponse IndexExists(this IElasticClient client,IIndexExistsRequest request);

		/// <inheritdoc />
		public static Task<ExistsResponse> IndexExistsAsync(this IElasticClient client,Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ExistsResponse> IndexExistsAsync(this IElasticClient client,IIndexExistsRequest request, CancellationToken ct = default);
	}


}
