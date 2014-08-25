using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TemplateExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse TemplateExists(Func<TemplateExistsDescriptor, TemplateExistsDescriptor> selector)
		{
			return this.Dispatch<TemplateExistsDescriptor, TemplateExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsTemplateDispatch<ExistsResponse>(
					p.DeserializationState(new TemplateExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse TemplateExists(ITemplateExistsRequest templateRequest)
		{
			return this.Dispatch<ITemplateExistsRequest, TemplateExistsRequestParameters, ExistsResponse>(
				templateRequest,
				(p, d) => this.RawDispatch.IndicesExistsTemplateDispatch<ExistsResponse>(
					p.DeserializationState(new TemplateExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> TemplateExistsAsync(Func<TemplateExistsDescriptor, TemplateExistsDescriptor> selector)
		{
			return this.DispatchAsync<TemplateExistsDescriptor, TemplateExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsTemplateDispatchAsync<ExistsResponse>(
					p.DeserializationState(new TemplateExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> TemplateExistsAsync(ITemplateExistsRequest templateRequest)
		{
			return this.DispatchAsync<ITemplateExistsRequest, TemplateExistsRequestParameters, ExistsResponse, IExistsResponse>(
				templateRequest,
				(p, d) => this.RawDispatch.IndicesExistsTemplateDispatchAsync<ExistsResponse>(
					p.DeserializationState(new TemplateExistConverter(DeserializeExistsResponse))
				)
			);
		}

	}
}