using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using static Nest.Infer;

namespace Nest
{
	using GetIndexTemplateConverter = Func<IApiCallDetails, Stream, GetIndexTemplateResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets an index template
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="selector">An optional selector specifying additional parameters for the get template operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetIndexTemplateResponse GetIndexTemplate(this IElasticClient client,
			Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null
		)
			=> client.Indices.GetTemplate(null, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetIndexTemplateResponse GetIndexTemplate(this IElasticClient client, IGetIndexTemplateRequest request)
			=> client.Indices.GetTemplate(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetIndexTemplateResponse> GetIndexTemplateAsync(this IElasticClient client,
			Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.GetTemplateAsync(null, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetIndexTemplateResponse> GetIndexTemplateAsync(this IElasticClient client, IGetIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.GetTemplateAsync(request, ct);
	}
}
