using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteResponse Delete<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) 
			where T : class
		{
			return this.Dispatcher.Dispatch<DeleteDescriptor<T>, DeleteRequestParameters, DeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatch<DeleteResponse>(p)
			);
		}

		/// <inheritdoc />
		public IDeleteResponse Delete(IDeleteRequest deleteRequest) 
		{
			return this.Dispatcher.Dispatch<IDeleteRequest, DeleteRequestParameters, DeleteResponse>(
				deleteRequest,
				(p, d) => this.RawDispatch.DeleteDispatch<DeleteResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<DeleteDescriptor<T>, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatchAsync<DeleteResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteAsync(IDeleteRequest deleteRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteRequest, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteRequest,
				(p, d) => this.RawDispatch.DeleteDispatchAsync<DeleteResponse>(p)
			);
		}

	}
}