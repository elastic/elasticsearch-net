using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc/>
		IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken));
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

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.RenderSearchTemplateAsync(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()), cancellationToken);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IRenderSearchTemplateRequest, RenderSearchTemplateRequestParameters, RenderSearchTemplateResponse, IRenderSearchTemplateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.RenderSearchTemplateDispatchAsync<RenderSearchTemplateResponse>(p, d, c)
			);
	}
}
