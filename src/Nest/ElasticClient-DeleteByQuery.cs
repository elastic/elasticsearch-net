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
			return this.Dispatch<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteByQueryResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.Dispatch<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.DispatchAsync<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest) 
		{
			return this.DispatchAsync<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				deleteByQueryRequest,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>(p, d)
			);
		}

	}
}