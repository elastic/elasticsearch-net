using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.Dispatcher.Dispatch<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest)
		{
			return this.Dispatcher.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.Dispatcher.DispatchAsync<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}

	}
}