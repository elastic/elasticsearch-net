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
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ExistsResponse IndexTemplateExists(this IElasticClient client, Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null
		)
			=> client.Indices.TemplateExists(template, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ExistsResponse IndexTemplateExists(this IElasticClient client, IIndexTemplateExistsRequest request)
			=> client.Indices.TemplateExists(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client, Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.TemplateExistsAsync(template, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client, IIndexTemplateExistsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.TemplateExistsAsync(request, ct);
	}
}
