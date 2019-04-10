using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html</a>
		/// </summary>
		/// <typeparam name="T">
		/// The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query
		/// </typeparam>
		/// <param name="selector">An optional descriptor to further describe the delete by query operation</param>
		IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest request);

		/// <inheritdoc />
		Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector) where T : class =>
			DeleteByQuery(selector?.Invoke(new DeleteByQueryDescriptor<T>(typeof(T))));

		/// <inheritdoc />
		public IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest request) =>
			DoRequest<IDeleteByQueryRequest, DeleteByQueryResponse>(request, request.RequestParameters, r => AcceptAllStatusCodesHandler(r));

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector,
			CancellationToken ct = default
		) where T : class =>
			DeleteByQueryAsync(selector?.Invoke(new DeleteByQueryDescriptor<T>(typeof(T))), ct);

		/// <inheritdoc />
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteByQueryRequest, IDeleteByQueryResponse, DeleteByQueryResponse>
				(request, request.RequestParameters, ct, r => AcceptAllStatusCodesHandler(r));
	}
}
