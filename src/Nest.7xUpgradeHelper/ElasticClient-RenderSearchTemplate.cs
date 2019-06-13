using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static RenderSearchTemplateResponse RenderSearchTemplate(this IElasticClient client,Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector);

		/// <inheritdoc />
		public static RenderSearchTemplateResponse RenderSearchTemplate(this IElasticClient client,IRenderSearchTemplateRequest request);

		/// <inheritdoc />
		public static Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(this IElasticClient client,Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(this IElasticClient client,IRenderSearchTemplateRequest request,
			CancellationToken ct = default
		);
	}

}
