using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IIndicesResponse DeleteIndex(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<DeleteIndexDescriptor, DeleteIndexRequestParameters, IndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteDispatch<IndicesResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesResponse DeleteIndex(IDeleteIndexRequest deleteIndexRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteIndexRequest, DeleteIndexRequestParameters, IndicesResponse>(
				deleteIndexRequest,
				(p, d) => this.RawDispatch.IndicesDeleteDispatch<IndicesResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> DeleteIndexAsync(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<DeleteIndexDescriptor, DeleteIndexRequestParameters, IndicesResponse, IIndicesResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteDispatchAsync<IndicesResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> DeleteIndexAsync(IDeleteIndexRequest deleteIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteIndexRequest, DeleteIndexRequestParameters, IndicesResponse, IIndicesResponse>(
				deleteIndexRequest,
				(p, d) => this.RawDispatch.IndicesDeleteDispatchAsync<IndicesResponse>(p)
			);
		}

	}
}