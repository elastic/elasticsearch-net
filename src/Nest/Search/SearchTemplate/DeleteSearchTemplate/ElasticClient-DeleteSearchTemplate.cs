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
		IDeleteSearchTemplateResponse DeleteSearchTemplate(string name, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(string name, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		public IDeleteSearchTemplateResponse DeleteSearchTemplate(string name, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) =>
			this.DeleteSearchTemplate(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor().Name(name)));

		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(string name, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) => 
			this.DeleteSearchTemplateAsync(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor().Name(name)));

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
	}
}
