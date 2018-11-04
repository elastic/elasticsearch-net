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
		IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null);

		/// <inheritdoc />
		IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request);

		/// <inheritdoc />
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null) =>
			GetIndexTemplate(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()));

		/// <inheritdoc />
		public IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request) =>
			Dispatcher.Dispatch<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesGetTemplateDispatch<GetIndexTemplateResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetIndexTemplateAsync(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse, IGetIndexTemplateResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.IndicesGetTemplateDispatchAsync<GetIndexTemplateResponse>(p, c)
				);
	}
}
