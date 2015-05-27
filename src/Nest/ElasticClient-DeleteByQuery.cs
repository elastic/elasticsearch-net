using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatcher.Dispatch<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IDeleteResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.Dispatcher.Dispatch<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatcher.DispatchAsync<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.Dispatcher.DispatchAsync<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteResponse>(p, d)
			);
		}

	}
}