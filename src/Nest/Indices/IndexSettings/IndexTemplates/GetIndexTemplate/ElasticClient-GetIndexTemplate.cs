using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using GetIndexTemplateConverter = Func<IApiCallDetails, Stream, GetIndexTemplateResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets an index template
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="selector">An optional selector specifying additional parameters for the get template operation</param>
		IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null);

		/// <inheritdoc/>
		IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request);

		/// <inheritdoc/>
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null) =>
			this.GetIndexTemplate(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()));

		/// <inheritdoc/>
		public IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest request)
		{
			return this.Dispatcher.Dispatch<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatch<GetIndexTemplateResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetIndexTemplateAsync(selector.InvokeOrDefault(new GetIndexTemplateDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse, IGetIndexTemplateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesGetTemplateDispatchAsync<GetIndexTemplateResponse>(p, c)
			);

	}
}
