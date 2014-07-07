using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteResponse Delete<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) where T : class
		{
			return this.Dispatch<DeleteDescriptor<T>, DeleteRequestParameters, DeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatch<DeleteResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector)
			where T : class
		{
			return this.DispatchAsync<DeleteDescriptor<T>, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteSelector,
				(p, d) => this.RawDispatch.DeleteDispatchAsync<DeleteResponse>(p)
			);
		}
	}
}