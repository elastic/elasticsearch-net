using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc />
		IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector) =>
			RenderSearchTemplate(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()));

		public IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request) =>
			Dispatcher.Dispatch<IRenderSearchTemplateRequest, RenderSearchTemplateRequestParameters, RenderSearchTemplateResponse>(
				request,
				(p, d) => LowLevelDispatch.RenderSearchTemplateDispatch<RenderSearchTemplateResponse>(p, d)
			);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(
			Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RenderSearchTemplateAsync(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()), cancellationToken);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IRenderSearchTemplateRequest, RenderSearchTemplateRequestParameters, RenderSearchTemplateResponse,
					IRenderSearchTemplateResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.RenderSearchTemplateDispatchAsync<RenderSearchTemplateResponse>(p, d, c)
				);
	}
}
