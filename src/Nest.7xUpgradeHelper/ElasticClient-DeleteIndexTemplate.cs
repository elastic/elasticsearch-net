using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an index template
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete</a>
		/// </summary>
		/// <param name="name">The name of the template to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete template operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client, Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null
		)
			=> client.Indices.DeleteTemplate(name, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client, IDeleteIndexTemplateRequest request)
			=> client.Indices.DeleteTemplate(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteTemplateAsync(name, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client, IDeleteIndexTemplateRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteTemplateAsync(request, ct);
	}
}
