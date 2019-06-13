using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client,Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc />
		public static PutIndexTemplateResponse PutIndexTemplate(this IElasticClient client,IPutIndexTemplateRequest request);

		/// <inheritdoc />
		public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<PutIndexTemplateDescriptor,
				IPutIndexTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutIndexTemplateResponse> PutIndexTemplateAsync(this IElasticClient client,IPutIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

}
