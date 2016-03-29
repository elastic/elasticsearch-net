using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc/>
		IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc/>
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request);
	}
	public partial class ElasticClient
	{
		public IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector) =>
			this.RenderSearchTemplate(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()));

		public IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request) =>
			this.Dispatcher.Dispatch<IRenderSearchTemplateRequest, RenderSearchTemplateRequestParameters, RenderSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.RenderSearchTemplateDispatch<RenderSearchTemplateResponse>(p, d)
			);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector) =>
			this.RenderSearchTemplateAsync(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()));

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request) =>
			this.Dispatcher.DispatchAsync<IRenderSearchTemplateRequest, RenderSearchTemplateRequestParameters, RenderSearchTemplateResponse, IRenderSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.RenderSearchTemplateDispatchAsync<RenderSearchTemplateResponse>(p, d)
			);
	}
}
