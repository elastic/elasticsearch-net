using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using AliasExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.AliasExists(), please update this usage.")]
		public static ExistsResponse AliasExists(this IElasticClient client, Names name,
			Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null
		)
			=> client.Indices.AliasExists(name, selector);

		[Obsolete("Moved to client.Indices.AliasExists(), please update this usage.")]
		public static ExistsResponse AliasExists(this IElasticClient client, IAliasExistsRequest request)
			=> client.Indices.AliasExists(request);

		[Obsolete("Moved to client.Indices.AliasExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> AliasExistsAsync(this IElasticClient client, Names name,
			Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.AliasExistsAsync(name, selector, ct);

		[Obsolete("Moved to client.Indices.AliasExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> AliasExistsAsync(this IElasticClient client, IAliasExistsRequest request, CancellationToken ct = default)
			=> client.Indices.AliasExistsAsync(request, ct);
	}
}
