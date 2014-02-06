using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IDeleteResponse Delete<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) where T : class
		{
			return this.Dispatch<DeleteDescriptor<T>, DeleteQueryString, DeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatch(p)
			);
		}

		public Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) where T : class
		{
			return this.DispatchAsync<DeleteDescriptor<T>, DeleteQueryString, DeleteResponse, IDeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatchAsync(p)
			);
		}
	
	}
}
