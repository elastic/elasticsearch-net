using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TemplateExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExistsResponse TemplateExists(Name template, Func<TemplateExistsDescriptor, ITemplateExistsRequest> selector = null);

		/// <inheritdoc/>
		IExistsResponse TemplateExists(ITemplateExistsRequest templateRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> TemplateExistsAsync(Name template, Func<TemplateExistsDescriptor, ITemplateExistsRequest> selector = null);

		/// <inheritdoc/>
		Task<IExistsResponse> TemplateExistsAsync(ITemplateExistsRequest templateRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse TemplateExists(Name template, Func<TemplateExistsDescriptor, ITemplateExistsRequest> selector = null) => 
			this.TemplateExists(selector.InvokeOrDefault(new TemplateExistsDescriptor(template)));

		/// <inheritdoc/>
		public IExistsResponse TemplateExists(ITemplateExistsRequest templateRequest) => 
			this.Dispatcher.Dispatch<ITemplateExistsRequest, TemplateExistsRequestParameters, ExistsResponse>(
				templateRequest,
				new TemplateExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTemplateDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> TemplateExistsAsync(Name template, Func<TemplateExistsDescriptor, ITemplateExistsRequest> selector = null) => 
			this.TemplateExistsAsync(selector.InvokeOrDefault(new TemplateExistsDescriptor(template)));

		/// <inheritdoc/>
		public Task<IExistsResponse> TemplateExistsAsync(ITemplateExistsRequest templateRequest)
		{
			return this.Dispatcher.DispatchAsync<ITemplateExistsRequest, TemplateExistsRequestParameters, ExistsResponse, IExistsResponse>(
				templateRequest,
				new TemplateExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTemplateDispatchAsync<ExistsResponse>(p)
			);
		}

	}
}