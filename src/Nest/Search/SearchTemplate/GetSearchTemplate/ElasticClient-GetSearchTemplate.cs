using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request);
	}
	public partial class ElasticClient
	{
		public IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) =>
			this.GetSearchTemplate(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)));

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) => 
			this.GetSearchTemplateAsync(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)));

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
	}
}
