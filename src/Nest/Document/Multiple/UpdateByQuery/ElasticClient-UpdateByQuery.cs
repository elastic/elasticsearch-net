using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The update by query API allows to update documents from one or more indices and one or more types based on a query.
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="selector">An optional descriptor to further describe the update by query operation</param>
		IUpdateByQueryResponse UpdateByQuery<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request);

		/// <inheritdoc/>
		Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateByQueryResponse UpdateByQuery<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector) where T : class =>
			this.UpdateByQuery(selector?.Invoke(new UpdateByQueryDescriptor<T>(typeof(T))));

		/// <inheritdoc/>
		public IUpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request) =>
			this.Dispatcher.Dispatch<IUpdateByQueryRequest, UpdateByQueryRequestParameters, UpdateByQueryResponse>(
				ForceConfiguration<IUpdateByQueryRequest, UpdateByQueryRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				this.LowLevelDispatch.UpdateByQueryDispatch<UpdateByQueryResponse>
			);

		/// <inheritdoc/>
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.UpdateByQueryAsync(selector?.Invoke(new UpdateByQueryDescriptor<T>(typeof(T))), cancellationToken);

		/// <inheritdoc/>
		public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IUpdateByQueryRequest, UpdateByQueryRequestParameters, UpdateByQueryResponse, IUpdateByQueryResponse>(
				ForceConfiguration<IUpdateByQueryRequest, UpdateByQueryRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				cancellationToken,
				this.LowLevelDispatch.UpdateByQueryDispatchAsync<UpdateByQueryResponse>
			);
	}
}
