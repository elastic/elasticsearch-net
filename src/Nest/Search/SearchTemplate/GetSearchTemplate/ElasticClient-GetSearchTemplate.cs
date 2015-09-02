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
		IGetSearchTemplateResponse GetSearchTemplate(string name, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(string name, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request);
	}
	public partial class ElasticClient
	{
		public IGetSearchTemplateResponse GetSearchTemplate(string name, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) => 
			this.Dispatcher.Dispatch<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				selector.InvokeOrDefault(new GetSearchTemplateDescriptor().Name(name)),
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(string name, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) => 
			this.Dispatcher.DispatchAsync<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				selector.InvokeOrDefault(new GetSearchTemplateDescriptor().Name(name)),
				(p, d) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
	}
}
