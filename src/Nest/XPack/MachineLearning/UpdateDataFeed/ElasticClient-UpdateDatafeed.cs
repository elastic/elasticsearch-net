using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Update a machine learning datafeed.
		/// </summary>
		IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request);

		/// <inheritdoc />
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class =>
			UpdateDatafeed(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc />
		public IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request) =>
			Dispatcher.Dispatch<IUpdateDatafeedRequest, UpdateDatafeedRequestParameters, UpdateDatafeedResponse>(
				request,
				LowLevelDispatch.MlUpdateDatafeedDispatch<UpdateDatafeedResponse>
			);

		/// <inheritdoc />
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			UpdateDatafeedAsync(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)), cancellationToken);

		/// <inheritdoc />
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IUpdateDatafeedRequest, UpdateDatafeedRequestParameters, UpdateDatafeedResponse, IUpdateDatafeedResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlUpdateDatafeedDispatchAsync<UpdateDatafeedResponse>
			);
	}
}
