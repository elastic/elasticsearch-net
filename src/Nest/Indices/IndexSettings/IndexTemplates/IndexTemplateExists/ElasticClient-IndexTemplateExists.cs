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
		ExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null);

		/// <inheritdoc />
		ExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request);

		/// <inheritdoc />
		Task<ExistsResponse> IndexTemplateExistsAsync(Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<ExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null) =>
			IndexTemplateExists(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)));

		/// <inheritdoc />
		public ExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request) =>
			DoRequest<IIndexTemplateExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexTemplateExistsAsync(Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			IndexTemplateExistsAsync(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)), cancellationToken);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IIndexTemplateExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
