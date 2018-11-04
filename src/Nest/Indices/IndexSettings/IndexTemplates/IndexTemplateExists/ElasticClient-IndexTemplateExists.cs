using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexTemplateExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null);

		/// <inheritdoc />
		IExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> IndexTemplateExistsAsync(Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null) =>
			IndexTemplateExists(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)));

		/// <inheritdoc />
		public IExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request) =>
			Dispatcher.Dispatch<IIndexTemplateExistsRequest, IndexTemplateExistsRequestParameters, ExistsResponse>(
				request,
				new IndexTemplateExistConverter(DeserializeExistsResponse),
				(p, d) => LowLevelDispatch.IndicesExistsTemplateDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexTemplateExistsAsync(Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IndexTemplateExistsAsync(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)), cancellationToken);

		/// <inheritdoc />
		public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) => Dispatcher.DispatchAsync<IIndexTemplateExistsRequest, IndexTemplateExistsRequestParameters, ExistsResponse, IExistsResponse>(
			request,
			cancellationToken,
			new IndexTemplateExistConverter(DeserializeExistsResponse),
			(p, d, c) => LowLevelDispatch.IndicesExistsTemplateDispatchAsync<ExistsResponse>(p, c)
		);
	}
}
