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
		/// <param name="putTemplateSelector">An optional selector specifying additional parameters for the put template operation</param>
		IIndicesOperationResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> putTemplateSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse PutIndexTemplate(IPutIndexTemplateRequest putTemplateRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> putTemplateSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest putTemplateRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse PutIndexTemplate(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> putTemplateSelector) => 
			this.PutIndexTemplate(putTemplateSelector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse PutIndexTemplate(IPutIndexTemplateRequest putTemplateRequest) => 
			this.Dispatcher.Dispatch<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, IndicesOperationResponse>(
				putTemplateRequest,
				this.LowLevelDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> putTemplateSelector) =>
			this.PutIndexTemplateAsync(putTemplateSelector.InvokeOrDefault(new PutIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest putTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IPutIndexTemplateRequest, PutIndexTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				putTemplateRequest,
				this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>
			);
	}
}