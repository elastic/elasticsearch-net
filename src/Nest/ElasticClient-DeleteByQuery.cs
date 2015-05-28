using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatcher.Dispatch<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteByQueryResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.Dispatcher.Dispatch<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQuerResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatcher.DispatchAsync<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.Dispatcher.DispatchAsync<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>(p, d)
			);
		}

	}
}