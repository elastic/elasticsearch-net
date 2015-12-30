using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Index templates allow to define templates that will automatically be applied to new indices created. 
		/// <para>The templates include both settings and mappings, and a simple pattern template that controls if 
		/// the template will be applied to the index created. </para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html
		/// </summary>
		/// <param name="name">The name of the template to register</param>
		/// <param name="selector">An optional selector specifying additional parameters for the put template operation</param>
		IAcknowledgedResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse PutIndexTemplate(IPutIndexTemplateRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector) => 
			this.PutIndexTemplate(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IAcknowledgedResponse PutIndexTemplate(IPutIndexTemplateRequest request) => 
			this.Dispatcher.Dispatch<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutTemplateDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector) =>
			this.PutIndexTemplateAsync(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<AcknowledgedResponse>
			);
	}
}