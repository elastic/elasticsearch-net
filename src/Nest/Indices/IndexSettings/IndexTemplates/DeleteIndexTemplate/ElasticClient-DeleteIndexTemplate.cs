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
		IDeleteIndexTemplateResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc />
		IDeleteIndexTemplateResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request);

		/// <inheritdoc />
		Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteIndexTemplateResponse DeleteIndexTemplate(
			Name name,
			Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null
		) => DeleteIndexTemplate(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc />
		public IDeleteIndexTemplateResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request) =>
			DoRequest<IDeleteIndexTemplateRequest, DeleteIndexTemplateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(
			Name name,
			Func<DeleteIndexTemplateDescriptor,
			IDeleteIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		) => DeleteIndexTemplateAsync(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)), ct);

		/// <inheritdoc />
		public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteIndexTemplateRequest, IDeleteIndexTemplateResponse, DeleteIndexTemplateResponse>(request, request.RequestParameters, ct);
	}
}
