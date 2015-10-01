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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete
		/// </summary>
		/// <param name="name">The name of the template to delete</param>
		/// <param name="deleteTemplateSelector">An optional selector specifying additional parameters for the delete template operation</param>
		IIndicesOperationResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> deleteTemplateSelector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest deleteTemplateRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> deleteTemplateSelector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest deleteTemplateRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> deleteTemplateSelector = null) =>
			this.DeleteIndexTemplate(deleteTemplateSelector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest deleteTemplateRequest) => 
			this.Dispatcher.Dispatch<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, IndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> deleteTemplateSelector = null) =>
			this.DeleteIndexTemplateAsync(deleteTemplateSelector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest deleteTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}