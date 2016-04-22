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
		IUpdateByQueryResponse UpdateByQuery<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request);

		/// <inheritdoc/>
		Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateByQueryResponse UpdateByQuery<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector) where T : class =>
			this.UpdateByQuery(selector?.Invoke(new UpdateByQueryDescriptor<T>(indices).Type(types)));

		/// <inheritdoc/>
		public IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request) =>
			this.Dispatcher.Dispatch<IUpdateByQueryRequest, UpdateByQueryRequestParameters, UpdateByQueryResponse>(
				this.ForceConfiguration<IUpdateByQueryRequest, UpdateByQueryRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				this.LowLevelDispatch.UpdateByQueryDispatch<UpdateByQueryResponse>
			);

		/// <inheritdoc/>
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector) where T : class =>
			this.UpdateByQueryAsync(selector?.Invoke(new UpdateByQueryDescriptor<T>(indices).Type(types)));

		/// <inheritdoc/>
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request) =>
			this.Dispatcher.DispatchAsync<IUpdateByQueryRequest, UpdateByQueryRequestParameters, UpdateByQueryResponse, IUpdateByQueryResponse>(
				this.ForceConfiguration<IUpdateByQueryRequest, UpdateByQueryRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				this.LowLevelDispatch.UpdateByQueryDispatchAsync<UpdateByQueryResponse>
			);
	}
}
