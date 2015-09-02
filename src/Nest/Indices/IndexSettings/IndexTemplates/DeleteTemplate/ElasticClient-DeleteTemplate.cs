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
		IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, IDeleteTemplateRequest> deleteTemplateSelector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse DeleteTemplate(IDeleteTemplateRequest deleteTemplateRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteTemplateAsync(string name, Func<DeleteTemplateDescriptor, IDeleteTemplateRequest> deleteTemplateSelector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteTemplateAsync(IDeleteTemplateRequest deleteTemplateRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, IDeleteTemplateRequest> deleteTemplateSelector = null) =>
			this.DeleteTemplate(deleteTemplateSelector.InvokeOrDefault(new DeleteTemplateDescriptor().Name(name)));

		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteTemplate(IDeleteTemplateRequest deleteTemplateRequest) => 
			this.Dispatcher.Dispatch<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(string name, Func<DeleteTemplateDescriptor, IDeleteTemplateRequest> deleteTemplateSelector = null) =>
			this.DeleteTemplateAsync(deleteTemplateSelector.InvokeOrDefault(new DeleteTemplateDescriptor().Name(name)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(IDeleteTemplateRequest deleteTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}