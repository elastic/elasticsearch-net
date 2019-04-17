using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		RenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc />
		RenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request);

		/// <inheritdoc />
		Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public RenderSearchTemplateResponse RenderSearchTemplate(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector) =>
			RenderSearchTemplate(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()));

		public RenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request) =>
			DoRequest<IRenderSearchTemplateRequest, RenderSearchTemplateResponse>(request, request.RequestParameters);

		public Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(
			Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken ct = default
		) =>
			RenderSearchTemplateAsync(selector.InvokeOrDefault(new RenderSearchTemplateDescriptor()), ct);

		public Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRenderSearchTemplateRequest, RenderSearchTemplateResponse>(request, request.RequestParameters, ct);
	}
}
