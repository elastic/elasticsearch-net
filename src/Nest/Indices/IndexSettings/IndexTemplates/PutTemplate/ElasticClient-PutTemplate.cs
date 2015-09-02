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
		IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, IPutTemplateRequest> putTemplateSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse PutTemplate(IPutTemplateRequest putTemplateRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, IPutTemplateRequest> putTemplateSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutTemplateAsync(IPutTemplateRequest putTemplateRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, IPutTemplateRequest> putTemplateSelector) => 
			this.PutTemplate(putTemplateSelector.InvokeOrDefault(new PutTemplateDescriptor(ConnectionSettings).Name(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse PutTemplate(IPutTemplateRequest putTemplateRequest) => 
			this.Dispatcher.Dispatch<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse>(
				putTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d.TemplateMapping)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, IPutTemplateRequest> putTemplateSelector) =>
			this.PutTemplateAsync(putTemplateSelector.InvokeOrDefault(new PutTemplateDescriptor(ConnectionSettings).Name(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutTemplateAsync(IPutTemplateRequest putTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				putTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d.TemplateMapping)
			);
	}
}