using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
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
		IPutIndexTemplateResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc />
		IPutIndexTemplateResponse PutIndexTemplate(IPutIndexTemplateRequest request);

		/// <inheritdoc />
		Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(
			Name name,
			Func<PutIndexTemplateDescriptor,
				IPutIndexTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutIndexTemplateResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector) =>
			PutIndexTemplate(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc />
		public IPutIndexTemplateResponse PutIndexTemplate(IPutIndexTemplateRequest request) =>
			DoRequest<IPutIndexTemplateRequest, PutIndexTemplateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(
			Name name,
			Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector,
			CancellationToken ct = default
		) => PutIndexTemplateAsync(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)), ct);

		/// <inheritdoc />
		public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutIndexTemplateRequest, IPutIndexTemplateResponse, PutIndexTemplateResponse>(request, request.RequestParameters, ct);
	}
}
