using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.PutTemplate(), please update this usage.")]
		public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client, Name name,
			Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector
		)
			=> client.Indices.PutTemplate(name, selector);

		[Obsolete("Moved to client.Indices.PutTemplate(), please update this usage.")]
		public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client, IPutIndexTemplateRequest request)
			=> client.Indices.PutTemplate(request);

		[Obsolete("Moved to client.Indices.PutTemplateAsync(), please update this usage.")]
		public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<PutIndexTemplateDescriptor,
				IPutIndexTemplateRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.PutTemplateAsync(name, selector, ct);

		[Obsolete("Moved to client.Indices.PutTemplateAsync(), please update this usage.")]
		public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client, IPutIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.PutTemplateAsync(request, ct);
	}
}
