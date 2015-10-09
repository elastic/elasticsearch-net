using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		public IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) =>
			this.DeleteSearchTemplate(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));


		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) => 
			this.DeleteSearchTemplateAsync(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
	}
}
