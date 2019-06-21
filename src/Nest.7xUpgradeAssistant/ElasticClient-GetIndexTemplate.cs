using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexTemplateConverter = Func<IApiCallDetails, Stream, GetIndexTemplateResponse>;

	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.GetTemplate(), please update this usage.")]
		public static GetIndexTemplateResponse GetIndexTemplate(this IElasticClient client,
			Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null
		)
			=> client.Indices.GetTemplate(null, selector);

		[Obsolete("Moved to client.Indices.GetTemplate(), please update this usage.")]
		public static GetIndexTemplateResponse GetIndexTemplate(this IElasticClient client, IGetIndexTemplateRequest request)
			=> client.Indices.GetTemplate(request);

		[Obsolete("Moved to client.Indices.GetTemplateAsync(), please update this usage.")]
		public static Task<GetIndexTemplateResponse> GetIndexTemplateAsync(this IElasticClient client,
			Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.GetTemplateAsync(null, selector, ct);

		[Obsolete("Moved to client.Indices.GetTemplateAsync(), please update this usage.")]
		public static Task<GetIndexTemplateResponse> GetIndexTemplateAsync(this IElasticClient client, IGetIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.GetTemplateAsync(request, ct);
	}
}
