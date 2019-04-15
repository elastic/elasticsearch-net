using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexTemplateConverter = Func<IApiCallDetails, Stream, GetIndexTemplateResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets an index template
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="selector">An optional selector specifying additional parameters for the get template operation</param>
		GetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null);

		/// <inheritdoc />
		GetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request);

		/// <inheritdoc />
		Task<GetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null) =>
			GetIndexTemplate(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()));

		/// <inheritdoc />
		public GetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request) =>
			DoRequest<IGetIndexTemplateRequest, GetIndexTemplateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetIndexTemplateResponse> GetIndexTemplateAsync(
			Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken ct = default
		) => GetIndexTemplateAsync(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetIndexTemplateRequest, GetIndexTemplateResponse, GetIndexTemplateResponse>(request, request.RequestParameters, ct);
	}
}
