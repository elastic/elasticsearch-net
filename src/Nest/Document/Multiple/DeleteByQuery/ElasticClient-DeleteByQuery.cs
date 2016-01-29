using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="selector">An optional descriptor to further describe the delete by query operation</param>
		IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest request);

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector) where T : class =>
			this.DeleteByQuery(selector?.Invoke(new DeleteByQueryDescriptor<T>(indices).Type(types)));

		/// <inheritdoc/>
		public IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest request) => 
			this.Dispatcher.Dispatch<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse>(
				request,
				this.LowLevelDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>
			);

		/// <inheritdoc/>
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector) where T : class =>
			this.DeleteByQueryAsync(selector?.Invoke(new DeleteByQueryDescriptor<T>(indices).Type(types)));

		/// <inheritdoc/>
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				request,
				this.LowLevelDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>
			);
	}
}