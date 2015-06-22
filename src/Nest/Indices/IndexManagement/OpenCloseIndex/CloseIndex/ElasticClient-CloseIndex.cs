using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatcher.Dispatch<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexSelector,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest)
		{
			return this.Dispatcher.Dispatch<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexRequest,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatcher.DispatchAsync
				<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexSelector,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexRequest,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

	}
}