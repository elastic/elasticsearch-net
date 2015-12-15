using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexTemplateExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null);

		/// <inheritdoc/>
		IExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null);

		/// <inheritdoc/>
		Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse IndexTemplateExists(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null) => 
			this.IndexTemplateExists(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)));

		/// <inheritdoc/>
		public IExistsResponse IndexTemplateExists(IIndexTemplateExistsRequest request) => 
			this.Dispatcher.Dispatch<IIndexTemplateExistsRequest, IndexTemplateExistsRequestParameters, ExistsResponse>(
				request,
				new IndexTemplateExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTemplateDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null) => 
			this.IndexTemplateExistsAsync(selector.InvokeOrDefault(new IndexTemplateExistsDescriptor(template)));

		/// <inheritdoc/>
		public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request)
		{
			return this.Dispatcher.DispatchAsync<IIndexTemplateExistsRequest, IndexTemplateExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				new IndexTemplateExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTemplateDispatchAsync<ExistsResponse>(p)
			);
		}

	}
}