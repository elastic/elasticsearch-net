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
		[Obsolete("Moved to client.Indices.Exists(), please update this usage.")]
		public static ExistsResponse IndexExists(this IElasticClient client, Indices indices,
			Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null
		)
			=> client.Indices.Exists(indices, selector);

		[Obsolete("Moved to client.Indices.Exists(), please update this usage.")]
		public static ExistsResponse IndexExists(this IElasticClient client, IIndexExistsRequest request)
			=> client.Indices.Exists(request);

		[Obsolete("Moved to client.Indices.ExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> IndexExistsAsync(this IElasticClient client, Indices indices,
			Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ExistsAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.ExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> IndexExistsAsync(this IElasticClient client, IIndexExistsRequest request, CancellationToken ct = default)
			=> client.Indices.ExistsAsync(request, ct);
	}
}
