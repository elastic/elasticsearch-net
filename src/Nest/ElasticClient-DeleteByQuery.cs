using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteResponse DeleteByQuery<T>(
			Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.Dispatch<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatch<DeleteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteByQueryAsync<T>(
			Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class
		{
			return this.DispatchAsync<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteByQuerySelector,
				(p, d) => this.RawDispatch.DeleteByQueryDispatchAsync<DeleteResponse>(p, d)
			);
		}
	}
}