using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.Dispatch<OpenIndexDescriptor, OpenIndexQueryString, IndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		public Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.DispatchAsync<OpenIndexDescriptor, OpenIndexQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}

		public IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatch<CloseIndexDescriptor, CloseIndexQueryString, IndicesOperationResponse>(
				closeIndexSelector,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		public Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.DispatchAsync<CloseIndexDescriptor, CloseIndexQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				closeIndexSelector,
				(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
			);
		}
	}
}
