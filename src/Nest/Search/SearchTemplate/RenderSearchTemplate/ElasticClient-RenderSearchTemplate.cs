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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public IRenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector) =>
			RenderSearchTemplate(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()));

		public IRenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request) =>
			DoRequest<IRenderSearchTemplateRequest, RenderSearchTemplateResponse>(request, request.RequestParameters);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(
			Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken ct = default
		) =>
			RenderSearchTemplateAsync(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()), ct);

		public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRenderSearchTemplateRequest, IRenderSearchTemplateResponse, RenderSearchTemplateResponse>(request, request.RequestParameters, ct);
	}
}
