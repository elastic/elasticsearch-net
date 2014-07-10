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
			return this.Dispatch<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest)
		{
			return this.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.DispatchAsync<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest)
		{
			return this.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}




		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatch<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexSelector,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest)
		{
			return this.Dispatch<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexRequest,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.DispatchAsync
				<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexSelector,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest)
		{
			return this.DispatchAsync<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexRequest,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

	}
}