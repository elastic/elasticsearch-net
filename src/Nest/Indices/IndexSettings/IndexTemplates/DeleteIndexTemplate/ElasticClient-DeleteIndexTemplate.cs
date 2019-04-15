using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an index template
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete</a>
		/// </summary>
		/// <param name="name">The name of the template to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete template operation</param>
		DeleteIndexTemplateResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc />
		DeleteIndexTemplateResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request);

		/// <inheritdoc />
		Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteIndexTemplateResponse DeleteIndexTemplate(
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null
		) => DeleteIndexTemplate(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc />
		public DeleteIndexTemplateResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request) =>
			DoRequest<IDeleteIndexTemplateRequest, DeleteIndexTemplateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(
			Name name,
			Func<DeleteIndexTemplateDescriptor,
			IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		) => DeleteIndexTemplateAsync(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)), ct);

		/// <inheritdoc />
		public Task<DeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteIndexTemplateRequest, DeleteIndexTemplateResponse>(request, request.RequestParameters, ct);
	}
}
