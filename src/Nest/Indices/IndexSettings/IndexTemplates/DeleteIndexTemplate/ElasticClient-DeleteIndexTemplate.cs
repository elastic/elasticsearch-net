using System;
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
		IAcknowledgedResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteIndexTemplate(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null) =>
			this.DeleteIndexTemplate(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteIndexTemplate(IDeleteIndexTemplateRequest request) => 
			this.Dispatcher.Dispatch<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector = null) =>
			this.DeleteIndexTemplateAsync(selector.InvokeOrDefault(new DeleteIndexTemplateDescriptor(name)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteIndexTemplateRequest, DeleteIndexTemplateRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}