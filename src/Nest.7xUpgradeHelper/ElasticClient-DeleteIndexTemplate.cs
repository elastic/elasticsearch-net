using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.DeleteTemplate(), please update this usage.")]
		public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client, Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null
		)
			=> client.Indices.DeleteTemplate(name, selector);

		[Obsolete("Moved to client.Indices.DeleteTemplate(), please update this usage.")]
		public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client, IDeleteIndexTemplateRequest request)
			=> client.Indices.DeleteTemplate(request);

		[Obsolete("Moved to client.Indices.DeleteTemplateAsync(), please update this usage.")]
		public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteTemplateAsync(name, selector, ct);

		[Obsolete("Moved to client.Indices.DeleteTemplateAsync(), please update this usage.")]
		public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client, IDeleteIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteTemplateAsync(request, ct);
	}
}
