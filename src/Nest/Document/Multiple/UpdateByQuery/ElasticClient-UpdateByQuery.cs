using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The update by query API allows to update documents from one or more indices and one or more types based on a query.
		/// </summary>
		/// <typeparam name="T">
		/// The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query
		/// </typeparam>
		/// <param name="selector">An optional descriptor to further describe the update by query operation</param>
		IUpdateByQueryResponse UpdateByQuery<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request);

		/// <inheritdoc />
		Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateByQueryResponse UpdateByQuery<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector) where T : class =>
			UpdateByQuery(selector?.Invoke(new UpdateByQueryDescriptor<T>(typeof(T))));

		/// <inheritdoc />
		public IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request) =>
			DoRequest<IUpdateByQueryRequest, UpdateByQueryResponse>(request, request.RequestParameters, r => AcceptAllStatusCodesHandler(r));

		/// <inheritdoc />
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(
			Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			UpdateByQueryAsync(selector?.Invoke(new UpdateByQueryDescriptor<T>(typeof(T))), ct);

		/// <inheritdoc />
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateByQueryRequest, IUpdateByQueryResponse, UpdateByQueryResponse>
				(request, request.RequestParameters, ct, r => AcceptAllStatusCodesHandler(r));
	}
}
