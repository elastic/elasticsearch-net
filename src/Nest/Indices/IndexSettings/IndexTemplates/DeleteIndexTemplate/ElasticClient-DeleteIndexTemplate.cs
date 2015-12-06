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
		/// Deletes an index template
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete</a>
		/// </summary>
		/// <param name="name">The name of the template to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete template operation</param>
		IIndicesOperationResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null) =>
			this.DeleteIndexTemplate(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request) => 
			this.Dispatcher.Dispatch<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, IndicesOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null) =>
			this.DeleteIndexTemplateAsync(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}