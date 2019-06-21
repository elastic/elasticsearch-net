using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Get(), please update this usage.")]
		public static GetIndexResponse GetIndex(this IElasticClient client, Indices indices,
			Func<GetIndexDescriptor, IGetIndexRequest> selector = null
		)
			=> client.Indices.Get(indices, selector);

		[Obsolete("Moved to client.Indices.Get(), please update this usage.")]
		public static GetIndexResponse GetIndex(this IElasticClient client, IGetIndexRequest request)
			=> client.Indices.Get(request);

		[Obsolete("Moved to client.Indices.GetAsync(), please update this usage.")]
		public static Task<GetIndexResponse> GetIndexAsync(this IElasticClient client, Indices indices,
			Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.GetAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.GetAsync(), please update this usage.")]
		public static Task<GetIndexResponse> GetIndexAsync(this IElasticClient client, IGetIndexRequest request, CancellationToken ct = default)
			=> client.Indices.GetAsync(request, ct);
	}
}
