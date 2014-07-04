using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse DocumentExists<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class
		{
			return this.Dispatch<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters, ExistsResponse>(
				d => existsSelector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => ToExistsResponse(this.RawDispatch.ExistsDispatch<VoidResponse>(p))
			);
		}

		/// <inheritdoc />
		public IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest)
		{
			return this.Dispatch<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse>(
				documentExistsRequest,
				(p, d) => ToExistsResponse(this.RawDispatch.ExistsDispatch<VoidResponse>(p))
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class
		{
			return this.DispatchAsync<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				d => existsSelector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => this.RawDispatch.ExistsDispatchAsync<ExistsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest)
		{
			return this.DispatchAsync<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				documentExistsRequest,
				(p, d) => this.RawDispatch.ExistsDispatchAsync<ExistsResponse>(p)
			);
		}

		private ElasticsearchResponse<ExistsResponse> ToExistsResponse(IElasticsearchResponse existsDispatch)
		{
			return ElasticsearchResponse.CloneFrom<ExistsResponse>(existsDispatch, new ExistsResponse(existsDispatch));
		}
	}
}