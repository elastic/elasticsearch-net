using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		IIndicesOperationResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc/>
		IIndicesOperationResponse PutIndexTemplate(IPutIndexTemplateRequest request);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector) => 
			this.PutIndexTemplate(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse PutIndexTemplate(IPutIndexTemplateRequest request) => 
			this.Dispatcher.Dispatch<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, IndicesOperationResponse>(
				request,
				this.LowLevelDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector) =>
			this.PutIndexTemplateAsync(selector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				request,
				this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>
			);
	}
}