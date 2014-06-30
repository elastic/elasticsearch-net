using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatch<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse>(
				d => selector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => this.RawDispatch.IndicesExistsDispatch<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.DispatchAsync<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				d => selector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		private ExistsResponse DeserializeExistsResponse(IElasticsearchResponse response, Stream stream)
		{
			return new ExistsResponse(response);
		}
	}
}