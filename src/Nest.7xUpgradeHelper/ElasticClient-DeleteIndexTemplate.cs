using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client,Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc />
		public static DeleteIndexTemplateResponse DeleteIndexTemplate(this IElasticClient client,IDeleteIndexTemplateRequest request);

		/// <inheritdoc />
		public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client,
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(this IElasticClient client,IDeleteIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

}
