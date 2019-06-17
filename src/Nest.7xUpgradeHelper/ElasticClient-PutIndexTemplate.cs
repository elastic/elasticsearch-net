using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Index templates allow to define templates that will automatically be applied to new indices created.
		/// <para>
		/// The templates include both settings and mappings, and a simple pattern template that controls if
		/// the template will be applied to the index created.
		/// </para>
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html
		/// </summary>
		/// <param name="name">The name of the template to register</param>
		/// <param name="selector">An optional selector specifying additional parameters for the put template operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client, Name name,
			Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector
		)
			=> client.Indices.PutTemplate(name, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client, IPutIndexTemplateRequest request)
			=> client.Indices.PutTemplate(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<PutIndexTemplateDescriptor,
				IPutIndexTemplateRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.PutTemplateAsync(name, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client, IPutIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.PutTemplateAsync(request, ct);
	}
}
