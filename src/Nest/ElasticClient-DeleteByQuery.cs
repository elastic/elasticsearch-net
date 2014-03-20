using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		public IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatch<DeleteByQueryDescriptor<T>, DeleteByQueryQueryString, DeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteResponse>(p, d)
			);
	  }

		public Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.DispatchAsync<DeleteByQueryDescriptor<T>, DeleteByQueryQueryString, DeleteResponse, IDeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteResponse>(p, d)
			);
		}

	}
}
