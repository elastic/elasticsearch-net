using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexTemplateExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.TemplateExists(), please update this usage.")]
		public static ExistsResponse IndexTemplateExists(this IElasticClient client, Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null
		)
			=> client.Indices.TemplateExists(template, selector);

		[Obsolete("Moved to client.Indices.TemplateExists(), please update this usage.")]
		public static ExistsResponse IndexTemplateExists(this IElasticClient client, IIndexTemplateExistsRequest request)
			=> client.Indices.TemplateExists(request);

		[Obsolete("Moved to client.Indices.TemplateExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client, Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.TemplateExistsAsync(template, selector, ct);

		[Obsolete("Moved to client.Indices.TemplateExistsAsync(), please update this usage.")]
		public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client, IIndexTemplateExistsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.TemplateExistsAsync(request, ct);
	}
}
